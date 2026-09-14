using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Raises the camera smoothly to follow upward player progress, without moving it back down.</summary>
public class CameraFollow : MonoBehaviour
{
    // The followed player and the interpolation rate configured per scene.
    [SerializeField] Player Player;
    [SerializeField] float Speed;
    private void FixedUpdate()
    {
        // Keep the death view stable once the player is marked dead.
        if (Player.isdead)
            return;

        if (Player.transform.position.y > transform.position.y)
        {
            // Preserve the camera's horizontal/depth coordinates; the game scrolls vertically only.
            Vector3 Positions = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Positions, Speed * Time.deltaTime);
        }

    }
}
