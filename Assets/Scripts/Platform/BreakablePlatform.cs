using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: Breaks, falls, and destroys itself when a descending player triggers it.
   Cheat sheet: StartCoroutine runs delayed steps; WaitForSeconds pauses a coroutine; gravityScale controls 2D gravity. */

/// <summary>Platform variant that animates, falls, and destroys itself after a player lands on it.</summary>
public class BreakablePlatform : MonoBehaviour
{
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
        // A descending player triggers the one-way collapse and its sound effect.
        if (other.CompareTag("Player") && other.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            StartCoroutine(BraekPlatform());
            other.GetComponent<Player>().BreakAblePlatformSFX();
        }

        IEnumerator BraekPlatform()
        {
            // Play the break animation, enable falling physics, then remove the platform after it leaves play.
            GetComponent<Animator>().SetTrigger("Break");
            yield return new WaitForSeconds(.25f);
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}
