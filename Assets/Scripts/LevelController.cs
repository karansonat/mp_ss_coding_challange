using System;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Vector3 _bossSpawnPosition;
    [SerializeField] private WaveFactory _defaultWaveFactory;
    [SerializeField] private WaveFactory _eliteWaveFactory;
    [SerializeField] private int _eliteRoundsBeforeBoss;
    [SerializeField] private int _defaultRoundRepeat;
    [SerializeField] private int _eliteRoundRepeat;
    private StateController _stateController;

    private int _completedEliteRound;
    private WaveType _lastWaveType;

    private enum WaveType
    {
        Default = 0,
        Elite = 1
    }

    #endregion //Fields

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.Instance.WaveStateCompleted += OnWaveStateCompleted;
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.WaveStateCompleted -= OnWaveStateCompleted;
        EventManager.Instance.GameOver -= OnGameOver;
    }

    private void Update()
    {
        if (_stateController != null)
            _stateController.Update();
    }

    #endregion //Unity Methods

    #region Public Methods

    public void Init()
    {
        _stateController = new StateController();
        //_stateController.Init(new WaveState(_defaultWaveFactory, _defaultRoundRepeat));
        _stateController.Init(new BossState(SpawnBoss()));
        _lastWaveType = WaveType.Default;
    }

    #endregion //Public Methods

    #region Private Methods

    private void OnWaveStateCompleted()
    {
        if (_lastWaveType == WaveType.Elite)
        {
            _completedEliteRound++;

            if (_completedEliteRound == _eliteRoundsBeforeBoss)
            {
                _stateController.SwitchState(new BossState(SpawnBoss()));
                return;
            }
        }

        switch (_lastWaveType)
        {
            case WaveType.Default:
                _stateController.SwitchState(new WaveState(_eliteWaveFactory, _defaultRoundRepeat));
                _lastWaveType = WaveType.Elite;
                break;
            case WaveType.Elite:
                _stateController.SwitchState(new WaveState(_defaultWaveFactory, _eliteRoundRepeat));
                _lastWaveType = WaveType.Default;
                break;
        }
    }

    private void OnGameOver()
    {
        if (_stateController != null)
            _stateController.Kill();
    }

    private BossController SpawnBoss()
    {
        return Instantiate(_bossPrefab, _bossSpawnPosition, Quaternion.identity).GetComponent<BossController>();
    }

    #endregion //Private Methods
}