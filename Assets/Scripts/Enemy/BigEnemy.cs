using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/* Script: Enemy variant that changes sprite when damaged while reusing the base enemy combat logic.
   Cheat sheet: inheritance extends Enemy; base.TakeDamage calls the parent method; override customizes inherited behavior. */

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
