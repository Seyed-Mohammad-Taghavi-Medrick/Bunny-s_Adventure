using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>Multi-hit enemy that reuses Enemy combat behavior and changes appearance when damaged.</summary>
public class BigEnemy : Enemy
{
    [FormerlySerializedAs("secenSprite")] [SerializeField] private Sprite damagedSprite;

    public override void TakeDamage(int damage)
    {
        // The sprite changes only when this hit leaves the enemy alive at one health.
        if (health - damage == 1 && damagedSprite != null && TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            spriteRenderer.sprite = damagedSprite;

        base.TakeDamage(damage);
    }
}
