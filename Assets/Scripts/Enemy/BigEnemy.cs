using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BigEnemy : MonoBehaviour
{
    [FormerlySerializedAs("secenSprite")] [SerializeField] public Sprite secendSprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg") && GetComponent<Enemy>().health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = secendSprite;
        }

       
    }
}