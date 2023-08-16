using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    private Rigidbody2D PlayerRigidBody;

    [SerializeField] private int power = 50;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().isJetpackenable = true;
           
        }
    }


  
}