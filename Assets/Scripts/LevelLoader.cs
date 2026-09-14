using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Central scene-navigation component, including splash delay and score submission when a run ends.</summary>
public class LevelLoader : MonoBehaviour
{
    // Splash-screen delay and the UI source for the leaderboard name.
    [SerializeField] private int timeToWait;
    [SerializeField] private InputField playerNameInputField;
    private int currentSceneIndext;
    private ScoreManagerSave scoreManagerSave;
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        // Cache scene services and identify the active build index for restart/next-scene navigation.
        scoreManagerSave = FindObjectOfType<ScoreManagerSave>();
        score = FindObjectOfType<Score>();

        currentSceneIndext = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndext == 0)
        {
            // Build index 0 is treated as the opening/splash scene.
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        // Delay in scaled time before automatically advancing from the splash scene.
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        // Reset time scale in case this action follows a paused state, then record the completed run.
        SceneManager.LoadScene(currentSceneIndext);
        Time.timeScale = 1;
        SaveScore();
    }

    public void LoadMainMenu()
    {
        // Returning to the menu also commits a named score if one is available.
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
        SaveScore();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndext + 1);
    }


    public void LoadLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SaveScore()
    {
        // Do not create anonymous leaderboard entries. The null check reflects the original scene wiring.
        if (!string.IsNullOrEmpty(playerNameInputField.text))
        {
            if (scoreManagerSave == null)
            {
                scoreManagerSave.AddScore(new ScoreSimple(playerNameInputField.text, score.GetPlayerScore));
            }
            
        }
        
        
        
    }
}
