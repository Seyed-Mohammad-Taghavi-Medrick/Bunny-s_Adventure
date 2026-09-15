using System.Collections.Generic;
using System;

[Serializable]
public class ScoreData
{
    public List<ScoreSimple> scores = new List<ScoreSimple>();
}

[Serializable]
public class ScoreSimple
{
    public string name;
    public int score;

    public ScoreSimple(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
