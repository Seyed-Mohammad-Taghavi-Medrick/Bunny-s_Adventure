using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>JSON-serializable container for all persisted leaderboard entries.</summary>
public class ScoreData
{

    public List<ScoreSimple> scores;

    public ScoreData()
    {
        // JsonUtility needs a concrete list so new score data can accept entries immediately.
        scores = new List<ScoreSimple>();
    }
}

[Serializable]
/// <summary>One leaderboard record: player name and the score to compare against that name's best run.</summary>
public class ScoreSimple
{
    public string name;
    public float score;

    public ScoreSimple(string name , float score)
    {
        this.name = name;
        this.score = score;
    }
}
