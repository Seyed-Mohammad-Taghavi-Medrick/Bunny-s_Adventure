using System.Collections.Generic;
using UnityEngine;

/* Script: Stores the best ten scores locally, sorts them, and exposes them to the leaderboard UI.
   Cheat sheet: JsonUtility converts simple data to JSON; List.Sort orders a list; const values cannot change at runtime. */

public class ScoreManager : MonoBehaviour
{
    // PlayerPrefs key and maximum number of records kept locally.
    private const string ScoresKey = "scores";
    private const int MaxScores = 10;

    private ScoreData scoreData;

    private void Awake()
    {
        // Load JSON when it exists; otherwise begin with an empty score list.
        string json = PlayerPrefs.GetString(ScoresKey, "");
        scoreData = string.IsNullOrEmpty(json) ? new ScoreData() : JsonUtility.FromJson<ScoreData>(json);

        if (scoreData == null || scoreData.scores == null)
            scoreData = new ScoreData();

        SortScores();
    }

    public void AddScore(string playerName, int playerScore)
    {
        // Ignore blank names so invalid leaderboard rows are not saved.
        if (string.IsNullOrWhiteSpace(playerName))
            return;

        scoreData.scores.Add(new ScoreSimple(playerName, playerScore));
        SortScores();
        SaveScores();
    }

    public List<ScoreSimple> GetHighScores()
    {
        // ScoreUI uses this ordered list to create its rows.
        return scoreData.scores;
    }

    private void SortScores()
    {
        // Higher scores appear first, then entries below the top ten are removed.
        scoreData.scores.Sort((first, second) => second.score.CompareTo(first.score));

        if (scoreData.scores.Count > MaxScores)
            scoreData.scores.RemoveRange(MaxScores, scoreData.scores.Count - MaxScores);
    }

    private void SaveScores()
    {
        // Convert the data wrapper to JSON and write it to local PlayerPrefs.
        PlayerPrefs.SetString(ScoresKey, JsonUtility.ToJson(scoreData));
        PlayerPrefs.Save();
    }
}
