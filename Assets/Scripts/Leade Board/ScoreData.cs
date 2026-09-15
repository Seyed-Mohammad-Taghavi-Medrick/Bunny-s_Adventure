using System.Collections.Generic;
using System;

/* Script: Defines serializable data types used to save the leaderboard as JSON.
   Cheat sheet: Serializable lets Unity convert data to JSON; a List stores multiple ScoreSimple records; a constructor initializes a new record. */

[Serializable]
public class ScoreData
{
    // JsonUtility needs this wrapper because it cannot serialize a list as the root value.
    public List<ScoreSimple> scores = new List<ScoreSimple>();
}

[Serializable]
public class ScoreSimple
{
    // One player's persisted name and score.
    public string name;
    public int score;

    public ScoreSimple(string name, int score)
    {
        // Store constructor arguments in this new score record.
        this.name = name;
        this.score = score;
    }
}
