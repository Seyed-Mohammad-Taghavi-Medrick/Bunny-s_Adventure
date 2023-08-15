using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
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
        if (other.CompareTag("Player") && other.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            StartCoroutine(BraekPlatform());
            other.GetComponent<Player>().BreakAblePlatformSFX();
        }

        IEnumerator BraekPlatform()
        {
            GetComponent<Animator>().SetTrigger("Break");
            yield return new WaitForSeconds(.25f);
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}