using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: A platform variant that gives a stronger upward launch after a downward landing.
   Cheat sheet: GetComponent reads a component on the same object; relativeVelocity indicates the impact direction. */

/// <summary>Platform variant that launches any descending collider with a stronger configurable vertical velocity.</summary>
public class JumpingBoard : MonoBehaviour
{
    Rigidbody2D PlayerRigidBody;
    // Upward velocity used instead of the base Platform bounce.
    [SerializeField] float power = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Only downward impacts activate the board and its animation.
        if ( other.relativeVelocity.y <= 0 )
        {
            GetComponent<Animator>().SetTrigger("jump");
            PlayerRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (PlayerRigidBody != null)
            {
                // Preserve horizontal movement while applying the stronger launch.
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, power);
            }
        }
    }
}
