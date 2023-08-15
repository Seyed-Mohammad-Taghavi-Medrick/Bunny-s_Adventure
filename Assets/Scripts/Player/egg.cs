using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class egg : MonoBehaviour
{
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
        transform.Translate(Vector3.up *Time.deltaTime *speed );
    }
}