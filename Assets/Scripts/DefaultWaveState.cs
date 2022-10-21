using UnityEngine;

public class DefaultWaveState : IState
{
    #region Fields

    private readonly string PATH_FACTORY = "Factories/DefaultWaveFactory";
    private WaveFactory _waveFactory;
    private float _startWait;
    private float _waveWait;
    private float _spawnTimer;
    private bool _readyToSpawn;
    private bool _enabled;
    private IState _nextState;

    #endregion //Fields

    #region Constructor

    public DefaultWaveState()
    {
        _enabled = false;
        _nextState = this;
    }

    #endregion //Constructor

    #region Interface Implementation

    void IState.Begin()
    {
        _readyToSpawn = false;
        _spawnTimer = _startWait;
        _waveFactory = Resources.Load<WaveFactory>(PATH_FACTORY);
        _startWait = _waveFactory.StartWait;
        _waveWait = _waveFactory.WaveWait;
        _enabled = true;
    }

    void IState.End()
    {
        _enabled = false;
    }

    IState IState.Update()
    {
        if (!_enabled)
            return _nextState;

        if (_readyToSpawn)
        {
            _waveFactory.SpawnWave(OnWaveSpawned);
            _readyToSpawn = false;
            _spawnTimer = _waveWait;
            _enabled = false;
        }
        else
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0f)
                _readyToSpawn = true;
        }

        return _nextState;
    }

    #endregion //Interface Implementation

    #region Private Methods

    private void OnWaveSpawned()
    {
        _enabled = true;
    }

    #endregion //Private Methods
}
