using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    #region Fields

    [Header("Canvases")]
    [SerializeField] private Canvas _canvasMainMenu;
    [SerializeField] private Canvas _canvasInGame;
    [SerializeField] private Canvas _canvasEndGame;

    [Header("Ui Fields")]
    [SerializeField] private Button _btnStart;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnContinue;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textEndGame;

    private Canvas _currentCanvas;
    private int _lastScore;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _btnStart.onClick.AddListener(OnStartButtonPressed);
        _btnRestart.onClick.AddListener(OnRestartButtonPressed);
        _btnContinue.onClick.AddListener(OnContinueButtonPressed);
        ShowCanvas(_canvasMainMenu);
    }

    private void OnEnable()
    {
        EventManager.Instance.ScoreUpdated += OnScoreUpdated;
        EventManager.Instance.GameStarted += OnGameStarted;
        EventManager.Instance.GameOver += OnGameOver;
        EventManager.Instance.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        EventManager.Instance.ScoreUpdated -= OnScoreUpdated;
        EventManager.Instance.GameStarted -= OnGameStarted;
        EventManager.Instance.GameOver -= OnGameOver;
        EventManager.Instance.LevelComplete -= OnLevelComplete;
    }

    #endregion //Unity Methods

    #region Private Methods

    private void ShowCanvas(Canvas canvas)
    {
        if (_currentCanvas != null)
            _currentCanvas.enabled = false;

        canvas.enabled = true;
        _currentCanvas = canvas;
    }

    private void OnStartButtonPressed()
    {
        EventManager.Instance.StartButtonPressed.Invoke();
    }

    private void OnContinueButtonPressed()
    {
        EventManager.Instance.RestartButtonPressed.Invoke();
    }

    private void OnRestartButtonPressed()
    {
        EventManager.Instance.RestartButtonPressed.Invoke();
    }

    #region Event Handlers

    private void OnScoreUpdated(int newScore, bool instant)
    {
        if (instant)
            _textScore.text = newScore.ToString();
        else
            DOTween.To(x => _textScore.text = ((int)x).ToString(), _lastScore, newScore, 0.5f);

        _lastScore = newScore;
    }

    private void OnGameStarted()
    {
        ShowCanvas(_canvasInGame);
    }

    private void OnGameOver()
    {
        ShowCanvas(_canvasEndGame);
        _textEndGame.text = "Game Over";
        _btnRestart.gameObject.SetActive(true);
    }

    private void OnLevelComplete()
    {
        ShowCanvas(_canvasEndGame);
        _textEndGame.text = "Level Complete";
        _btnContinue.gameObject.SetActive(true);
    }

    #endregion //Event Handlers

    #endregion //Private Methods
}
