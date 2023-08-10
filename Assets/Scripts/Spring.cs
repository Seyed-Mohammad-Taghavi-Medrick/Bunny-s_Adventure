using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    Rigidbody2D PlayerRigidBody;

    [SerializeField] float power = 15f;
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
        if (other.relativeVelocity.y <= 0)
        {
            GetComponent<Animator>().SetTrigger("jump");
            PlayerRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (PlayerRigidBody != null)
            {
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, power);
            }
        }
    }
}