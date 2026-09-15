using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Script: Converts the player's highest vertical progress into a score and updates the score UI.
   Cheat sheet: property get exposes read-only data; uint stores non-negative whole numbers; FixedUpdate follows the physics tick. */

[Serializable]
/// <summary>Tracks upward player progress as the run score and mirrors it into the in-game and holder UI texts.</summary>
public class Score : MonoBehaviour
{
    // UI targets and the tracked player transform. Score is derived from the player's vertical position.
    [SerializeField]  GameObject playerScoreHolder; 
    [SerializeField] Transform Player;
    [SerializeField] float Speed;
    [SerializeField] uint PlayerScore;
    public Text ScoreTxt;

    public uint GetPlayerScore
    {
        // Read-only public access used when a completed run is submitted to the leaderboard.
        get => PlayerScore;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    private void Start()
    {
        // Show the serialized/current score immediately before the first physics update.
        UpdateUI();
    }


    private void FixedUpdate()

    {
        if (Player.position.y > transform.position.y)
        {
            // Score and UI advance only while the player sets a new vertical high point.
            Vector3 Positions = new Vector3(transform.position.x, Player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Positions, Speed * Time.deltaTime);
            PlayerScore = (uint)Player.position.y;
            UpdateUI();

            /* PlayerScore++;
            ScoreTxt.text = PlayerScore.ToString();*/
        }

    }

    private void UpdateUI()
    {
        // Keep both score displays identical; the holder is the value consumed by other scene/UI wiring.
        
        ScoreTxt.text = "" + PlayerScore;

        playerScoreHolder.GetComponent<Text>().text = ScoreTxt.text;
    }
}
