using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: Wraps the player from one horizontal edge of the level to the other.
   Cheat sheet: CompareTag checks a tag efficiently; OnTriggerEnter2D runs when a Collider2D enters a trigger. */

/// <summary>One half of the horizontal wrap-around boundary: entering it moves the player near the opposite boundary.</summary>
public class Mirrior : MonoBehaviour
{
    [SerializeField] private Player _player;
    // Offset keeps the teleported player inside the destination side instead of immediately retriggering.
    [SerializeField] private float distance = 5f; 

    [SerializeField] private GameObject otherSide;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only the player wraps; enemies and projectiles are unaffected by this trigger.
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position =
                new Vector3(otherSide.transform.position.x + distance, other.transform.position.y, other.transform.position.z);
        }
    }
}
