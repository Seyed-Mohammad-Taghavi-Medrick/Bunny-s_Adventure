using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Loads leaderboard data, keeps each player's best result, and serializes it back to PlayerPrefs.</summary>
public class ScoreManagerSave : MonoBehaviour
{
   private ScoreData sd;

   private void Awake()
   {
      // اگر داده‌ای وجود نداشت (یا خراب بود)، از یک لیست خالی شروع کن.
      var json = PlayerPrefs.GetString("scores", "");
      sd = string.IsNullOrEmpty(json) ? new ScoreData() : JsonUtility.FromJson<ScoreData>(json);
      if (sd == null || sd.scores == null)
         sd = new ScoreData();
   }

   public void AddScore(ScoreSimple score)
   {
      // Each name has one record: only replace it if this run improved the previous score.
      var record = sd.scores.SingleOrDefault(x => x.name == score.name); 
      
      if (record != null)
      {
         if (score.score > record.score)
         {
            sd.scores.Remove(record);
            sd.scores.Add(score);
         }
      }
      else
      {
         sd.scores.Add(score);
      }

      SaveScore();
   }

   void SaveScore()
   {
      // Serialize the full score list as one PlayerPrefs value for later leaderboard scenes.
      var  json  = JsonUtility.ToJson(sd);
      PlayerPrefs.SetString ("scores" , json);
      PlayerPrefs.Save();
   }
}
