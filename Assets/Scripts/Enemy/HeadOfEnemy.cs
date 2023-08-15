using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, Platform.power);
            
        }


        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponentInParent<BigEnemy>())
            {
                GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<BigEnemy>().secendSprite;
            }

            var enemy = GetComponentInParent<Enemy>();


            enemy.health -= 1;


            if (enemy.health <= 0)
            {
                enemy.TriggerDeathVFX(Parent.transform.position);
                Destroy(Parent);
            }
        }
    }
}