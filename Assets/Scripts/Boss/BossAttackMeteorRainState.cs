using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackMeteorRainState : IState
{
    #region Fields

    [SerializeField] private BossController _boss;
    private IState _currentState;
    private int _destroyedWaveElementCount;

    #endregion //Fields

    #region Constructor

    public BossAttackMeteorRainState(BossController boss)
    {
        _boss = boss;
        _currentState = this;
    }

    #endregion //Constructor

    #region IState Interface

    void IState.Begin()
    {
        Debug.Log("BossAttackMeteorRainState");
        _boss.SetShield(true);
        _boss.MeteorWave.SpawnWave();
        _destroyedWaveElementCount = 0;
        EventManager.Instance.WaveElementDestoryed += OnWaveElementDestoryed;
    }

    void IState.End()
    {
        _boss.SetShield(false);
        EventManager.Instance.WaveElementDestoryed -= OnWaveElementDestoryed;
    }

    IState IState.Update()
    {
        return _currentState;
    }

    #endregion //IState Interface

    #region Private Methods

    private void OnWaveElementDestoryed()
    {
        _destroyedWaveElementCount++;

        if (_destroyedWaveElementCount == _boss.MeteorWave.WaveSize)
        {
            _currentState = new BossIdleState(_boss);
        }
    }

    #endregion //Private Methods
}
