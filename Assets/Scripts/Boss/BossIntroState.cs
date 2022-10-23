using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossIntroState : IState
{
    #region Fields

    [SerializeField] private BossController _boss;
    private IState _currentState;

    #endregion //Fields

    #region Constructor

    public BossIntroState(BossController boss)
    {
        _boss = boss;
        _currentState = this;
    }

    #endregion //Constructor

    #region IState Interface

    void IState.Begin()
    {
        _boss.SetShield(true);
        _boss.SetManeuver(false);
        var introMovementDistance = 7f;
        var targetPos = _boss.transform.position + Vector3.back * introMovementDistance;
        _boss.transform.DOMove(targetPos, 2f).SetEase(Ease.OutSine).OnComplete(OnIntroMovementComplete);
    }

    void IState.End()
    {
        _boss.SetShield(false);
        _boss.SetManeuver(true);
    }

    IState IState.Update()
    {
        return _currentState;
    }

    #endregion //IState Interface

    #region Private Methods

    private void OnIntroMovementComplete()
    {
        _currentState = new BossIdleState(_boss);
    }

    #endregion //Private Methods
}