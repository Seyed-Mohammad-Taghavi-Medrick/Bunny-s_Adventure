using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>بارگذاری صحنه‌ها و ذخیره‌ی امتیاز پایان هر دور بازی.</summary>
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
        // این دو شیء فقط در صحنه‌ی پایان بازی وجود دارند؛ در منو چیزی ذخیره نمی‌کنیم.
        if (scoreManager == null || score == null || playerNameInputField == null)
            return;

        string playerName = playerNameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
            scoreManager.AddScore(playerName, (int)score.GetPlayerScore);
    }
}
