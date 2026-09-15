using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
