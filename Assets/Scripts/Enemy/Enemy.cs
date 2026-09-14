using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

/// <summary>Defines an enemy's health, collision responses, optional hole behavior, and death visual effect.</summary>
public class Enemy : MonoBehaviour
{
    private Vector3 centerPosition;
    // Inspector-configured combat/visual settings shared by normal and large enemy prefabs.
    [SerializeField] public int health;
    [SerializeField] public GameObject VFX;
    [SerializeField] public bool isHole;
    [SerializeField] private GameObject stars;
    public bool isPalayerDamaged;

    public bool enemyDamaged;
   
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hole enemies delegate their player interaction to Hole; normal enemies process eggs and player contact here.
        if (!isHole)
        {
            if (collision.gameObject.tag == "Egg")
            {
                // Projectile impact consumes the egg and subtracts one health point.
                enemyDamaged = true;
               
                health -= 1;
                enemyDamaged = false;
                Destroy(collision.gameObject);

                if (health <= 0)
                {
                    // Death VFX is spawned before the enemy object is removed.
                    TriggerDeathVFX(transform.position);
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Player")
            {
                // Shielded players can pass through enemies without entering the death state.
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
        // Kept public so HeadOfEnemy can use the same visual death path.
        Instantiate(VFX, targetPosition, quaternion.identity);
    }

    public void SetOriginPosition(Vector3 pos)
    {
        // Spawn origin is retained for manager/recycling logic.
        centerPosition = pos;
    }

    public Vector3 GetOriginPosition()
    {
        return centerPosition;
    }

   
}
