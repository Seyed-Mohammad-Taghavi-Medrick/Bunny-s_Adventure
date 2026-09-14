using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>Pickup trigger that grants the player the shield state; GameManager owns its duration and visuals.</summary>
public class Shield : MonoBehaviour
{
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
        // A shield pickup only affects the player; the pickup's destruction/recycling is handled elsewhere.
        if (!(gameObject == null))
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Player>().isShieldEnable = true;
            }
        }
    }
}
