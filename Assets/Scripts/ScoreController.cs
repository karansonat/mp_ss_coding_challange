using UnityEngine.UI;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _scoreText;

    private int _score;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _score = 0;
        UpdateScore();
    }

    private void OnEnable()
    {
        EventManager.Instance.ScoreEarned += OnScoreEarned;
    }

    private void OnDisable()
    {
        EventManager.Instance.ScoreEarned -= OnScoreEarned;
    }

    #endregion //Unity Methods

    #region Private Methods

    private void AddScore(int earnedScore)
    {
        _score += earnedScore;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Score: {_score}";
    }

    #region Event Handlers

    private void OnScoreEarned(int earnedScore)
    {
        AddScore(earnedScore);
    }

    #endregion //Event Handlers

    #endregion //Private Methods
}
