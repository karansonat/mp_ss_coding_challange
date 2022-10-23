using UnityEngine;

public class BossState : IState
{
    #region Fields

    private StateController _stateController;
    private BossController _boss;
    private bool _isBossKilled;

    #endregion //Fields

    #region Constructor

    public BossState(BossController boss)
    {
        _boss = boss;
        _isBossKilled = false;
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
        if (!_isBossKilled)
            _stateController.Update();

        return this;
    }

    #endregion //IState Interface

    #region Private Methods

    private void OnBossKilled()
    {
        _isBossKilled = true;
        _stateController.Kill();
        Debug.Log("Level Complete");
    }

    #endregion //Private Methods
}
