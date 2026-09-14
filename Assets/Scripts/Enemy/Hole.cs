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
        // The repeated trigger callback continuously pulls the player into the hole until the death flow takes over.
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isPlayerDamaged = true;
            // Stop physics-driven escape and visually interpolate toward the hazard center.
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.transform.position =
                Vector3.Lerp(other.transform.position, gameObject.transform.position, Time.deltaTime * 10);
            other.transform.localScale = Vector3.Lerp(other.transform.localScale, other.transform.localScale / 2,
                Time.deltaTime * 10);
            other.gameObject.transform.rotation = Quaternion.Lerp(other.transform.rotation,
                other.transform.rotation * quaternion.RotateZ(2),
                Time.deltaTime * 10);
        }
    }
}
