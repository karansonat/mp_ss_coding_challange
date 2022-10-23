using UnityEngine;

public class BossState : IState
{
    #region Fields

    private StateController _stateController;
    private BossController _boss;

    #endregion //Fields

    #region Constructor

    public BossState(BossController boss)
    {
        _boss = boss;
    }

    #endregion //Constructor

    #region IState Interface

    void IState.Begin()
    {
        _stateController = new StateController();
        _stateController.Init(new BossIntroState(_boss));
        EventManager.Instance.BossKilled += OnBossKilled;
    }

    void IState.End()
    {
        EventManager.Instance.BossKilled -= OnBossKilled;
    }

    IState IState.Update()
    {
        _stateController.Update();
        return this;
    }

    #endregion //IState Interface

    #region Private Methods

    private void OnBossKilled()
    {
        Debug.Log("Level Complete");
    }

    #endregion //Private Methods
}
