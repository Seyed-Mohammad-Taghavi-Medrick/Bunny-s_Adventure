using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Base data component for generated power-up instances. It records their original spawn location.</summary>
public class PowerUp : MonoBehaviour
{
    private Vector3 centerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetOriginPosition(Vector3 pos)
    {
        // Stored for potential repositioning/recycling; the current manager destroys passed pickups instead.
        centerPosition = pos;
    }
}
