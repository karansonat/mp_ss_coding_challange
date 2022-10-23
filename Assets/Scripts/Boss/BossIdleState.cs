using UnityEngine;

public class BossIdleState : IState
{
    #region Fields

    [SerializeField] private BossController _boss;
    private IState _currentState;
    private const float TIMER_MAX = 2f;
    private float _timer;

    #endregion //Fields

    #region Constructor

    public BossIdleState(BossController boss)
    {
        _boss = boss;
        _currentState = this;
    }

    #endregion //Constructor

    #region IState Interface

    void IState.Begin()
    {
        Debug.Log("Boss Idle");
        _timer = TIMER_MAX;
    }

    void IState.End()
    {
    }

    IState IState.Update()
    {
        if (IdleTimerComplete())
            return new BossAttackRapidFireState(_boss);

        return _currentState;
    }

    #endregion //IState Interface

    #region Private Methods

    private bool IdleTimerComplete()
    {
        _timer -= Time.deltaTime;
        return _timer <= 0f;
    }

    #endregion //Private Methods
}