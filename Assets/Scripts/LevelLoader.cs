using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script: Loads game scenes and saves a finished run to the leaderboard.
   Cheat sheet: SceneManager changes scenes; FindObjectOfType finds a scene component; Trim removes outer spaces from text. */

/// <summary>Loads scenes and saves a completed run score.</summary>
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int timeToWait;
    [SerializeField] private InputField playerNameInputField;

    private int currentSceneIndext;
    private ScoreManager scoreManager;
    private Score score;

    private void Start()
    {
        currentSceneIndext = SceneManager.GetActiveScene().buildIndex;
        scoreManager = FindObjectOfType<ScoreManager>();
        score = FindObjectOfType<Score>();

        if (currentSceneIndext == 0)
            StartCoroutine(WaitForTime());
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        SaveScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndext);
    }

    public void LoadMainMenu()
    {
        SaveScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadNextScene() => SceneManager.LoadScene(currentSceneIndext + 1);
    public void LoadLeaderBoard() => SceneManager.LoadScene("LeaderBoard");
    public void LoadLoseScreen() => SceneManager.LoadScene("Lose Screen");
    public void LoadOptionsScreen() => SceneManager.LoadScene("Options");
    public void QuitGame() => Application.Quit();

    private void SaveScore()
    {
        // These objects exist only after a run; menu scenes have no score to save.
        if (scoreManager == null || score == null || playerNameInputField == null)
            return;

        string playerName = playerNameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
            scoreManager.AddScore(playerName, (int)score.GetPlayerScore);
    }
}
