using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Base platform behavior: bounces a landing player unless a specialized spring/jumping-board component handles it.</summary>
public class Platform : MonoBehaviour
{
    // Shared default bounce velocity, also referenced by the enemy-head bounce behavior.
    [SerializeField] public static float power = 10f;
    Rigidbody2D PlayerRigidBody;
    [SerializeField, Range(0, 1)] private float creationChance = 1f;
   


    [SerializeField] bool oneTime = false;

    private Vector3 centerPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Springs own their landing response, so this base bounce is skipped when one is a child.
        if (!GetComponentInChildren<Spring>())
        {
            if (collision.relativeVelocity.y <= 0 && collision.gameObject.CompareTag("Player"))
            {
                // Require a descending player and preserve horizontal velocity when applying the upward bounce.
                PlayerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (PlayerRigidBody != null && GetComponent<JumpingBoard>() == null && GetComponent<Spring>() == null)
                {
                   
                    PlayerRigidBody.velocity += new Vector2(PlayerRigidBody.velocity.x, power);
                    PlayerRigidBody.GetComponent<Player>().PlayJumpSFX();
                }
            }
        }
    }

    public void SetOriginPosition(Vector3 pos)
    {
        // Stores the spawn point for generation/recycling systems.
        centerPosition = pos;
    }

    public Vector3 GetOriginPosition()
    {
        return centerPosition;
    }

    public float GetCreationChance()
    {
        // Exposes the per-prefab creation weighting retained for generation logic.
        return creationChance;
    }
}
