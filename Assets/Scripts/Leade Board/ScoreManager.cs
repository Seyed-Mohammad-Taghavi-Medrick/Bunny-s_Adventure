using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string ScoresKey = "scores";
    private const int MaxScores = 10;

    private ScoreData scoreData;

    private void Awake()
    {
        string json = PlayerPrefs.GetString(ScoresKey, "");
        scoreData = string.IsNullOrEmpty(json) ? new ScoreData() : JsonUtility.FromJson<ScoreData>(json);

        if (scoreData == null || scoreData.scores == null)
            scoreData = new ScoreData();

        SortScores();
    }

    public void AddScore(string playerName, int playerScore)
    {
        if (string.IsNullOrWhiteSpace(playerName))
            return;

        scoreData.scores.Add(new ScoreSimple(playerName, playerScore));
        SortScores();
        SaveScores();
    }

    public List<ScoreSimple> GetHighScores()
    {
        return scoreData.scores;
    }

    private void SortScores()
    {
        scoreData.scores.Sort((first, second) => second.score.CompareTo(first.score));

        if (scoreData.scores.Count > MaxScores)
            scoreData.scores.RemoveRange(MaxScores, scoreData.scores.Count - MaxScores);
    }

    private void SaveScores()
    {
        PlayerPrefs.SetString(ScoresKey, JsonUtility.ToJson(scoreData));
        PlayerPrefs.Save();
    }
}
