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
        /*StartCoroutine(ChangeCollider2d);*/
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0 && other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("jump");
            PlayerRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (PlayerRigidBody != null)
            {
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, power);
                PlayerRigidBody.GetComponent<Player>().PlayspringSFX();
                if (PlayerRigidBody.velocity.y >= 0)
                {
                    /*IEnumerator ChangeCollider2d()
                    {
                        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        yield new WaitForSeconds(.2f);
                        other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }*///**/
                }
            }
        }
    }
}