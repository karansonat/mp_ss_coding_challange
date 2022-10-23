using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRapidFireState : IState
{
    #region Fields

    [SerializeField] private BossController _boss;
    private IState _currentState;
    private const float TIMER_MAX = 5f;
    private float _timer;

    #endregion //Fields

    #region Constructor

    public BossAttackRapidFireState(BossController boss)
    {
        _boss = boss;
        _currentState = this;
    }

    #endregion //Constructor

    #region IState Interface

    void IState.Begin()
    {
        Debug.Log("BossAttackRapidFireState");
        _timer = TIMER_MAX;
        _boss.SetWeapons(true);
    }

    void IState.End()
    {
        _boss.SetWeapons(false);
    }

    IState IState.Update()
    {
        if (TimerComplete())
            return new BossAttackMeteorRainState(_boss);

        return _currentState;
    }

    #endregion //IState Interface

    #region Private Methods

    private bool TimerComplete()
    {
        _timer -= Time.deltaTime;
        return _timer <= 0f;
    }

    #endregion //Private Methods
}
