using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 centerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*var player = collision.gameObject.GetComponent<Palyer>();
            player.Die();*/
        }
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