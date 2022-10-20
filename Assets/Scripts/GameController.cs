using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    public void Start() {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        GetComponent<WaveController>().Init();
    }

    public void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public void GameOver() {
        gameOverText.text = "Game Over!";
        restartText.text = "Press 'R' for Restart";
        gameOver = true;
        restart = true;
        EventManager.Instance.GameOver.Invoke();
    }
}