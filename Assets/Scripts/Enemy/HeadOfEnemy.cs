using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Enemy weak-point collider: bounces the player and applies one damage to its parent enemy.</summary>
public class HeadOfEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Parent;

    private Rigidbody2D PlayerRigidBody;

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
        // A landing player receives the standard platform-like upward bounce.
        if (other.gameObject.CompareTag("Player"))
        {
            // Big enemies switch to their damaged sprite when their head is hit.
            PlayerRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, Platform.power);
            
        }


        if (other.gameObject.CompareTag("Player"))
        {
            var enemy = GetComponentInParent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(1);
        }
    }
}
