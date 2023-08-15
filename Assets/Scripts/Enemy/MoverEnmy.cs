using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        randomPos = Random.Range(-1, 1);
        platform = GetComponent<Platform>();
    }

    void FixedUpdate()
    {

        Vector3 Top_Left = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var maxDistance = -Top_Left.x - Offset;
        transform.position = new Vector3(Mathf.Sin((Time.time + randomPos) * speed / maxDistance) * maxDistance, /*platform.GetOriginPosition().y*/ gameObject.transform.position.y, 0);

    }
}