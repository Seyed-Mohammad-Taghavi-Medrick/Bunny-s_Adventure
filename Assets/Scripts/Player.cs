using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private egg[] eggs;
    [SerializeField] private GameObject projectile;
    private Vector2 movment;
    private float _nextFire;
    [SerializeField] private float fireRate = .5f;

    private Rigidbody2D playerRigid;

    [SerializeField] public bool isdead;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");


        movment = new Vector2(inputX * 10, playerRigid.velocity.y);
    }

    private void FixedUpdate()
    {
        playerRigid.velocity = movment;

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            egg bullet = eggs[Random.Range(0, eggs.Length)];
            Instantiate(bullet, projectile.transform.position, projectile.transform.rotation);

        }
    }
}