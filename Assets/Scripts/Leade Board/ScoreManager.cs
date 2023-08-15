using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   private ScoreData sd;

   private void Awake()
   {
      var json = PlayerPrefs.GetString("scores", "{}");
      sd = JsonUtility.FromJson<ScoreData>(json);
   }

   public IEnumerable<ScoreSimple> GetHighScore()
   {
      return sd.scores.OrderByDescending(x => x.score);
   }

}