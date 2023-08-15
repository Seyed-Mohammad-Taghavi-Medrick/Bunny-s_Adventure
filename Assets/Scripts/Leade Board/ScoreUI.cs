using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;


    private void Start()
    {
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
