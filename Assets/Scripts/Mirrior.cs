using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirrior : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float distance = 5f; 

    [SerializeField] private GameObject otherSide;

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
        if (gameObject.CompareTag("Player"))
        {
            other.transform.position =
                new Vector3(otherSide.transform.position.x + distance, other.transform.position.y, other.transform.position.z);
        }
    }
}