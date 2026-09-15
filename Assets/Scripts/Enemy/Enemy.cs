using UnityEngine;
using UnityEngine.Serialization;

/* Script: Base enemy with health, projectile/player collision logic, optional hole behavior, and death effects.
   Cheat sheet: virtual methods can be overridden by child classes; TryGetComponent safely reads a component; protected is visible to children. */

/// <summary>Defines an enemy's health, collision responses, optional hole behavior, and death visual effect.</summary>
public class Enemy : MonoBehaviour
{
    private Vector3 centerPosition;
    // Inspector-configured combat/visual settings shared by normal and large enemy prefabs.
    [SerializeField] protected int health;
    [FormerlySerializedAs("VFX")] [SerializeField] private GameObject vfx;
    [SerializeField] private bool isHole;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Hole enemies delegate their player interaction to Hole; normal enemies process eggs and player contact here.
        if (isHole)
            return;

        if (collision.CompareTag("Egg"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            return;
        }

        if (collision.CompareTag("Player") &&
            collision.TryGetComponent<Player>(out var player) &&
            !player.isShieldEnable && !player.isJetpackenable)
            player.isPlayerDamaged = true;
    }

    /// <summary>Shared damage entry point for projectiles and enemy weak points.</summary>
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        TriggerDeathVFX(transform.position);
        Destroy(gameObject);
    }

    public void TriggerDeathVFX(Vector3 targetPosition)
    {
        if (vfx != null)
            Instantiate(vfx, targetPosition, Quaternion.identity);
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
