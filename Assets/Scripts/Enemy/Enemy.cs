using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Vector3 centerPosition;
    [SerializeField] public int health;
    [SerializeField] public GameObject VFX;
    [SerializeField] public bool isHole;
    [SerializeField] private GameObject stars;
    public bool isPalayerDamaged;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHole)
        {
            if (collision.gameObject.tag == "Egg")
            {
                health -= 1;
                Destroy(collision.gameObject);

                if (health <= 0)
                {
                    TriggerDeathVFX(transform.position);
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Player")
            {
                if (!(FindObjectOfType<Player>().isShieldEnable))
                {
                    
                FindObjectOfType<Player>().isPlayerDamaged = true;
                }

                /*stars = collision.GetComponentInChildren<Stars>().gameObject;
                stars.SetActive(true);*/
                /*TriggerDeathVFX(collision.transform.position);*/
            }
        }
    }

    public void TriggerDeathVFX(Vector3 targetPosition)
    {
        Instantiate(VFX, targetPosition, quaternion.identity);
    }

    public void SetOriginPosition(Vector3 pos)
    {
        centerPosition = pos;
    }

    public Vector3 GetOriginPosition()
    {
        return centerPosition;
    }
}