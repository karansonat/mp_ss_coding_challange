using UnityEngine;

public class StateController
{
    #region Fields

    private IState _currentState;

    #endregion //Fields

    #region Public Methods

    public void Init(IState initialState)
    {
        SwitchState(initialState);
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void SwitchState(IState newState)
    {
        if (_currentState != null)
            _currentState.End();

        _currentState = newState;
        _currentState.Begin();
    }

    public void Kill()
    {
        if (_currentState != null)
            _currentState.End();
    }

    #endregion //Public Methods
}
