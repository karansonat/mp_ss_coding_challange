using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region Fields

    private int _score;
    private double _multiplier;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _multiplier = 1;
        _score = 0;
        UpdateScore();
    }

    private void OnEnable()
    {
        EventManager.Instance.ScoreEarned += OnScoreEarned;
        EventManager.Instance.ScoreMultiplierUpdated += OnScoreMultiplierUpdated;
    }

    private void OnDisable()
    {
        EventManager.Instance.ScoreEarned -= OnScoreEarned;
        EventManager.Instance.ScoreMultiplierUpdated -= OnScoreMultiplierUpdated;
    }

    #endregion //Unity Methods

    #region Private Methods

    private void AddScore(int earnedScore)
    {
        _score += Mathf.FloorToInt(earnedScore * (float)_multiplier);
        UpdateScore();
    }

    private void UpdateScore()
    {
        EventManager.Instance.ScoreUpdated.Invoke(_score, _score == 0);
    }

    #region Event Handlers

    private void OnScoreEarned(int earnedScore)
    {
        AddScore(earnedScore);
    }

    private void OnScoreMultiplierUpdated(double multiplier)
    {
        _multiplier = multiplier;
    }

    #endregion //Event Handlers

    #endregion //Private Methods
}
