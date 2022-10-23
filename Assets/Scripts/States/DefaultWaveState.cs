using System;
using UnityEngine;

public class WaveState : IState
{
    #region Fields

    private WaveFactory _waveFactory;
    private float _startWait;
    private float _waveWait;
    private float _spawnTimer;
    private bool _readyToSpawn;
    private bool _enabled;
    private int _spawnedWaveCount;
    private int _maxWaveCount;
    private int _destroyedWaveElementCount;

    #endregion //Fields

    #region Constructor

    public WaveState(WaveFactory waveFactory, int waveCount)
    {
        _enabled = false;
        _waveFactory = waveFactory;
        _maxWaveCount = waveCount;
    }

    #endregion //Constructor

    #region Interface Implementation

    void IState.Begin()
    {
        _destroyedWaveElementCount = 0;
        _spawnedWaveCount = 0;
        _readyToSpawn = false;
        _spawnTimer = _startWait;
        _startWait = _waveFactory.StartWait;
        _waveWait = _waveFactory.WaveWait;
        _enabled = true;
        EventManager.Instance.WaveElementDestoryed += OnWaveElementDestoryed;
    }

    void IState.End()
    {
        _enabled = false;
        EventManager.Instance.WaveElementDestoryed -= OnWaveElementDestoryed;
    }

    IState IState.Update()
    {
        if (!_enabled)
            return this;

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

        return this;
    }

    #endregion //Interface Implementation

    #region Private Methods

    private void OnWaveSpawned()
    {
        _spawnedWaveCount++;

        if (_spawnedWaveCount != _maxWaveCount)
            _enabled = true;
    }

    #region Event Handlers

    private void OnWaveElementDestoryed()
    {
        _destroyedWaveElementCount++;

        if (_destroyedWaveElementCount == _waveFactory.WaveSize)
            EventManager.Instance.WaveStateCompleted.Invoke();
    }

    #endregion //Event Handlers

    #endregion //Private Methods
}
