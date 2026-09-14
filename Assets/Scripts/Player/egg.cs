using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>Simple upward-moving projectile spawned by Shooter. Collision and destruction are handled by enemies.</summary>
public class egg : MonoBehaviour
{
    // Constant world-space movement speed along the projectile's local up axis.
    [SerializeField] private int speed = 5; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        // Rotation set by Shooter determines the actual travel direction.
        transform.Translate(Vector3.up *Time.deltaTime *speed );
    }
}
