using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _scoreText;

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
        _scoreText.text = $"Score: {_score}";
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
