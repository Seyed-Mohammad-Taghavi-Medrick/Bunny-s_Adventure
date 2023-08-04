using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float power = 7.5f;
    Rigidbody2D PlayerRigidBody;
    [SerializeField, Range(0, 1)] private float creationChance = 1f;
    

    [SerializeField] bool oneTime = false;

    private Vector3 centerPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            PlayerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (PlayerRigidBody != null)
            {
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, power);
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