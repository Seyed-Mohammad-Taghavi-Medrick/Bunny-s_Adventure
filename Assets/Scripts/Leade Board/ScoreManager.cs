using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Read-only leaderboard service that loads saved scores and exposes them in descending order.</summary>
public class ScoreManager : MonoBehaviour
{
   private ScoreData sd;

   private void Awake()
   {
      // Use the same PlayerPrefs key and JSON shape as ScoreManagerSave.
      var json = PlayerPrefs.GetString("scores", "");
      sd = string.IsNullOrEmpty(json) ? new ScoreData() : JsonUtility.FromJson<ScoreData>(json);
      if (sd == null || sd.scores == null)
         sd = new ScoreData();
   }

   public IEnumerable<ScoreSimple> GetHighScore()
   {
      // Deferred LINQ ordering is consumed by ScoreUI when it creates rows.
      return sd.scores.OrderByDescending(x => x.score);
   }

}
