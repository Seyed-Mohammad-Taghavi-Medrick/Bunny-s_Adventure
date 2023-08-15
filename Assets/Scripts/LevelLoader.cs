using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int timeToWait;
    [SerializeField] private InputField playerNameInputField;
    private int currentSceneIndext;
    private ScoreManagerSave scoreManagerSave;
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        scoreManagerSave = FindObjectOfType<ScoreManagerSave>();
        score = FindObjectOfType<Score>();
        
        currentSceneIndext = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndext == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndext);
        Time.timeScale = 1;
        SaveScore();
    }

    public void LoadMainMenu()
    {
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
        if (!string.IsNullOrEmpty( playerNameInputField.text))
        {
            scoreManagerSave.AddScore(new ScoreSimple(playerNameInputField.text,score.GetPlayerScore ));
        }
    }
    
}