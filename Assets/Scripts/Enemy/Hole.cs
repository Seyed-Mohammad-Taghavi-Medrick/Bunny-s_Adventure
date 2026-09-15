using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>Hazard trigger that kills the player and animates a pull/shrink/rotation effect while they remain inside it.</summary>
public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        var player = other.GetComponent<Player>();
        var playerRigidbody = other.GetComponent<Rigidbody2D>();
        if (player == null || playerRigidbody == null || player.isPlayerDamaged)
            return;

        // Keep the collider active during this effect; GameManager disables it only once the pull completes.
        player.isBeingPulledIntoHole = true;
        player.isJetpackenable = false;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.gravityScale = 0;
        other.transform.position = Vector3.Lerp(other.transform.position, transform.position, Time.deltaTime * 10f);
        other.transform.localScale = Vector3.Lerp(other.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
        other.transform.rotation = Quaternion.Lerp(other.transform.rotation,
            other.transform.rotation * Quaternion.Euler(0f, 0f, 2f), Time.deltaTime * 10f);

        if (other.transform.localScale.sqrMagnitude <= 0.01f)
        {
            player.isBeingPulledIntoHole = false;
            player.isPlayerDamaged = true;
        }
    }
}
