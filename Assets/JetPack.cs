using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

/// <summary>Pickup trigger that enables the player's jetpack state; its timed presentation is managed by GameManager.</summary>
public class JetPack : MonoBehaviour
{
    private Rigidbody2D PlayerRigidBody;

    // Kept as a prefab tuning value; the current movement force is applied by Player, not this component.
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
        // Signal the Player component rather than applying force here, so all jetpack movement remains centralized.
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().isJetpackenable = true;
           
        }
    }


  
}
