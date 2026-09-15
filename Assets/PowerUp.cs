using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: Base class for every power-up, with shared player detection and an overridable effect method.
   Cheat sheet: protected is available to child classes; virtual allows children to customize a method; TryGetComponent safely finds a component. */

/// <summary>Base class for all generated power-ups. It provides common placement and pickup handling.</summary>
public class PowerUp : MonoBehaviour
{
    private Vector3 centerPosition;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
            Apply(player);
    }

    /// <summary>Override in a concrete pickup to grant its player effect.</summary>
    protected virtual void Apply(Player player) { }

    public void SetOriginPosition(Vector3 pos)
    {
        // Stored for potential repositioning/recycling; the current manager destroys passed pickups instead.
        centerPosition = pos;
    }
}
