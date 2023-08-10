using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 centerPosition;
    [SerializeField] public int health;
    [SerializeField] private GameObject VFX;
    [SerializeField] private bool isHole;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHole)
        {
            if (collision.gameObject.CompareTag("Egg"))
            {
                health -= 1;
                
                if (health <= 0)
                {
                    TriggerDeathVFX();
                    Destroy(collision);
                    Destroy(gameObject);
                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(collision.GetComponent<Collider2D>());
            }
        }
    }

    void TriggerDeathVFX()
    {
        Instantiate(VFX, transform.position, quaternion.identity);
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