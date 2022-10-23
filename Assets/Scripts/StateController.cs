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
        if (_currentState == null)
            return;

        var state = _currentState.Update();

        if (state != _currentState)
            SwitchState(state);
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
