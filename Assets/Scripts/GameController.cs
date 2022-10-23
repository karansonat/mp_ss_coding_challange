using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameController : Singleton<GameController>
{

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.Instance.StartButtonPressed += OnStartButtonPressed;
        EventManager.Instance.RestartButtonPressed += OnRestartButtonPressed;
    }

    private void OnDisable()
    {
        EventManager.Instance.StartButtonPressed -= OnStartButtonPressed;
        EventManager.Instance.RestartButtonPressed -= OnRestartButtonPressed;
    }

    #endregion //Unity Methods

    #region Public Methods

    public void GameOver()
    {
        EventManager.Instance.GameOver.Invoke();
    }

    #endregion //Public Methods

    #region Private Methods

    private void InitGame()
    {
        GetComponent<LevelController>().Init();
        EventManager.Instance.GameStarted.Invoke();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Event Handlers

    private void OnStartButtonPressed()
    {
        InitGame();
    }

    private void OnRestartButtonPressed()
    {
        RestartGame();
    }

    #endregion //Event Handlers

    #endregion //Private Methods
}