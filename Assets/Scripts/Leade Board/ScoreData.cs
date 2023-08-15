using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData
{

    public List<ScoreSimple> scores;

    public ScoreData()
    {
        scores = new List<ScoreSimple>();
    }
}

[Serializable]
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