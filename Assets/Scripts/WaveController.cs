using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    #region Fields

    private StateController _stateController;

    #endregion //Fields

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.GameOver -= OnGameOver;
    }

    private void Update()
    {
        if (_stateController != null)
            _stateController.Update();
    }

    #endregion //Unity Methods

    #region Public Methods

    public void Init()
    {
        _stateController = new StateController();
        _stateController.Init(new DefaultWaveState());
    }

    #endregion //Public Methods

    #region Private Methods

    private void OnGameOver()
    {
        if (_stateController != null)
            _stateController.Kill();
    }

    #endregion //Private Methods
}