using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: Moves an enemy side to side in a sine-wave pattern based on camera width.
   Cheat sheet: Mathf.Sin creates smooth oscillation; Random.Range gives each enemy a different phase; FixedUpdate runs on the physics tick. */

/// <summary>Moves its object side-to-side using a sine wave whose range follows the camera's visible width.</summary>
public class MoverEnmy : MonoBehaviour
{

    // For moving platforms
    private bool To_Right = true;
    [SerializeField] private float Offset = 1.2f;
    [SerializeField] private float multiplier = 2;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float velocity;

    private Vector3 lastPos;
    private Platform platform;
    private float randomPos;
    private void Start()
    {
        // Per-instance phase variation prevents all moving objects from oscillating in lockstep.
        randomPos = Random.Range(-1, 1);
        platform = GetComponent<Platform>();
    }

    void FixedUpdate()
    {
        // Convert the lower-left screen point to world space to determine the horizontal travel limit.

        Vector3 Top_Left = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var maxDistance = -Top_Left.x - Offset;
        // Keep vertical position fixed while the sine function supplies the horizontal offset.
        transform.position = new Vector3(Mathf.Sin((Time.time + randomPos) * speed / maxDistance) * maxDistance, /*platform.GetOriginPosition().y*/ gameObject.transform.position.y, 0);

    }
}
