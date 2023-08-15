using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Score : MonoBehaviour
{
    [SerializeField]  GameObject playerScoreHolder; 
    [SerializeField] Transform Player;
    [SerializeField] float Speed;
    [SerializeField] uint PlayerScore;
    public Text ScoreTxt;

    public uint GetPlayerScore
    {
        get => PlayerScore;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    private void Start()
    {
        UpdateUI();
    }


    private void FixedUpdate()

    {
        if (Player.position.y > transform.position.y)
        {
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
        
        ScoreTxt.text = "" + PlayerScore;

        playerScoreHolder.GetComponent<Text>().text = ScoreTxt.text;
    }
}