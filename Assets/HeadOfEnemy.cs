using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOfEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Parent.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
