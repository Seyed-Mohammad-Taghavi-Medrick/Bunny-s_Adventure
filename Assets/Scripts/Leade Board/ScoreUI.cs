using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Creates one leaderboard UI row per saved score, ordered by ScoreManager.</summary>
public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;


    private void Start()
    {
        // Materialize the ordered enumerable once, then populate rank, name, and numeric score for each row.
        var scorse = scoreManager.GetHighScore().ToArray();
        for (int i = 0; i < scorse.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();

            row.rank.text = (i + 1).ToString();
            row.name.text = scorse[i].name;
            row.score.text = scorse[i].score.ToString();
        }
    }
}
