using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private PlayerEventManager _eventManager;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;
	
    private Rigidbody body;

    private const string FIRE_BTN = "Fire1";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public void Awake ()
	{
        body = GetComponent<Rigidbody>();
		_eventManager = new PlayerEventManager();
		GetComponent<PlayerInteractionHandler>().Init(_eventManager);
	}

    private void OnEnable()
    {
		_eventManager.BonusScore += OnBonusScore;
    }

    private void OnDisable()
    {
		_eventManager.BonusScore -= OnBonusScore;
	}

    public void Update () {
		if (Input.GetButton(FIRE_BTN) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	public void FixedUpdate () {
		float moveHorizontal = Input.GetAxis (HORIZONTAL);
		float moveVertical = Input.GetAxis (VERTICAL);

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * speed;
		
		body.position = new Vector3(
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (body.position.z, boundary.zMin, boundary.zMax)
		);
		
		body.rotation = Quaternion.Euler (0.0f, 0.0f, body.velocity.x * -tilt);
	}

	#region Event Handlers

	private void OnBonusScore(int score)
	{
		EventManager.Instance.ScoreEarned?.Invoke(score);
	}

	#endregion //Event Handlers
}
