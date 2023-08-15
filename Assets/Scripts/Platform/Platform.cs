using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] public static float power = 10f;
    Rigidbody2D PlayerRigidBody;
    [SerializeField, Range(0, 1)] private float creationChance = 1f;
   


    [SerializeField] bool oneTime = false;

    private Vector3 centerPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GetComponentInChildren<Spring>())
        {
            if (collision.relativeVelocity.y <= 0 && collision.gameObject.CompareTag("Player"))
            {
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
        centerPosition = pos;
    }

    public Vector3 GetOriginPosition()
    {
        return centerPosition;
    }

    public float GetCreationChance()
    {
        return creationChance;
    }
}