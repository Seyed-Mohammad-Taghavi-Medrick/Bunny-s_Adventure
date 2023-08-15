using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManagerSave : MonoBehaviour
{
   private ScoreData sd;

   private void Awake()
   {
      var json = PlayerPrefs.GetString("scores", "{}");
      sd = JsonUtility.FromJson<ScoreData>(json);
   }

   public void AddScore(ScoreSimple score)
   {
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
      var  json  = JsonUtility.ToJson(sd);
      PlayerPrefs.SetString ("scores" , json);
   }
}
