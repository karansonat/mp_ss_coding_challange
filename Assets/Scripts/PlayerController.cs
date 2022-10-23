using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private PlayerEventManager _eventManager;
	private float _currentSpeed;
	private Coroutine _speedMultiplierRoutine;
	private Coroutine _scoreMultiplierRoutine;

	public float speed;
	public float tilt;
	public Boundary boundary;	
    private Rigidbody body;
	private Vector3 _initialPosition;
	private Collider _hitBox;
	private bool _enabled;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public void Awake ()
	{
		_initialPosition = transform.position;
        body = GetComponent<Rigidbody>();
		_eventManager = new PlayerEventManager();
		GetComponent<PlayerInteractionHandler>().Init(_eventManager);
		_currentSpeed = speed;
		_hitBox = GetComponent<Collider>();
	}

    private void OnEnable()
    {
		_eventManager.BonusScore += OnBonusScore;
		_eventManager.TimedUpgrade += OnTimedUpgrade;
		_eventManager.ItemUnlocked += OnItemUnlocked;
		EventManager.Instance.GameStarted += OnGameStarted;
		EventManager.Instance.LevelComplete += OnLevelComplete;
		
	}

	private void OnDisable()
    {
		_eventManager.BonusScore -= OnBonusScore;
		_eventManager.TimedUpgrade -= OnTimedUpgrade;
		_eventManager.ItemUnlocked -= OnItemUnlocked;
		EventManager.Instance.GameStarted -= OnGameStarted;
		EventManager.Instance.LevelComplete -= OnLevelComplete;
	}

    public void FixedUpdate ()
	{
		if (!_enabled)
			return;

		float moveHorizontal = Input.GetAxis (HORIZONTAL);
		float moveVertical = Input.GetAxis (VERTICAL);

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * _currentSpeed;
		
		body.position = new Vector3(
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (body.position.z, boundary.zMin, boundary.zMax)
		);
		
		body.rotation = Quaternion.Euler (0.0f, 0.0f, body.velocity.x * -tilt);
	}

	private IEnumerator TimedUpgradeSpeed(TimedUpgradeData data, double duration)
    {
		_currentSpeed = speed * (float)data.Multiplier;

		yield return new WaitForSeconds((float)duration);

		_currentSpeed = speed;
    }

	private IEnumerator TimedUpgradeScore(TimedUpgradeData data, double duration)
	{
		EventManager.Instance.ScoreMultiplierUpdated.Invoke(data.Multiplier);

		yield return new WaitForSeconds((float)duration);

		EventManager.Instance.ScoreMultiplierUpdated.Invoke(1f);
	}

	private void StartNewUpgradeRoutine(ref Coroutine currentRoutine, IEnumerator upgradeFunc)
    {
		if (currentRoutine != null)
			StopCoroutine(currentRoutine);

		currentRoutine = StartCoroutine(upgradeFunc);
	}

	#region Event Handlers

	private void OnBonusScore(int score)
	{
		EventManager.Instance.ScoreEarned?.Invoke(score);
	}

	private void OnTimedUpgrade(TimedUpgradeData data, double duration)
	{
        switch (data.Type)
        {
            case TimedUpgradeType.Speed:
				StartNewUpgradeRoutine(ref _speedMultiplierRoutine, TimedUpgradeSpeed(data, duration));
				break;
            case TimedUpgradeType.FireRate:
                break;
            case TimedUpgradeType.Health:
                break;
			case TimedUpgradeType.Score:
				StartNewUpgradeRoutine(ref _scoreMultiplierRoutine, TimedUpgradeScore(data, duration));
				break;
		}
	}

	private void OnItemUnlocked(UnlockableItemData itemData)
	{
		EventManager.Instance.ItemUnlocked?.Invoke(itemData);
	}

	private void OnGameStarted()
	{
		_enabled = true;
	}

	private void OnLevelComplete()
	{
		_enabled = false;
		_hitBox.enabled = false;
		body.velocity = Vector3.zero;
		var seq = DOTween.Sequence();
		seq.Append(transform.DOMove(_initialPosition, 2f));
		seq.Join(transform.DORotate(Quaternion.identity.eulerAngles, 2f));
	}

	#endregion //Event Handlers
}
