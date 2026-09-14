using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Moves a platform horizontally along a camera-width sine wave, with a per-instance phase offset.</summary>
public class LineMovment : MonoBehaviour
{

    // For moving platforms
    /*private bool To_Right = true;*/
    [SerializeField] private float Offset = 1.2f;
    [SerializeField] private float multiplier = 2;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float velocity;

    private Vector3 lastPos;
    private Platform platform;
    private float randomPos;
    private void Start()
    {
        // Random phase stops all moving platforms from reaching their endpoints together.
        randomPos = Random.Range(-1, 1);
        platform = GetComponent<Platform>();
    }

    void FixedUpdate()
    {
        // Camera bounds determine a movement span that stays aligned with the visible play area.

        Vector3 Top_Left = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var maxDistance = -Top_Left.x - Offset;
        // Keep the generated Y coordinate unchanged and update only the horizontal position.
        transform.position = new Vector3(Mathf.Sin((Time.time + randomPos) * speed / maxDistance) * maxDistance, /*platform.GetOriginPosition().y*/ gameObject.transform.position.y, 0);

    }
}
