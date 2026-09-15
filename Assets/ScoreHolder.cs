using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script: Legacy score UI holder. It keeps the old scene component valid but has no active behavior.
   Cheat sheet: MonoBehaviour lets a class live on a GameObject; SerializeField keeps a private Inspector reference. */

/// <summary>Legacy score-holder component retained in the scene. Its former UI synchronization code is intentionally inactive.</summary>
public class ScoreHolder : MonoBehaviour
{

    [SerializeField] private Score playerScore;

    // Start is called before the first frame update
    void Start()
    {
        /*GetComponent<Text>().text = playerScore.GetComponent<Text>();
    }*/

        // Update is called once per frame
        void Update()
        {

        }
    }
}
