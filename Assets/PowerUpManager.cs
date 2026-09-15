using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script: Builds an upward sequence of power-up pickups and replaces pickups that leave the play area.
   Cheat sheet: Random.Range picks values or array indexes; Instantiate makes a prefab copy; OnTriggerEnter2D reacts to trigger overlap. */

/// <summary>Builds a vertical chain of power-up pickups and replenishes it as pickups leave the active area.</summary>
public class PowerUpManager : MonoBehaviour
{
    // Vertical gap bounds and horizontal spawn limit; the score gradually increases both gap bounds.
    [SerializeField] private float maxPowerUpDistance = 100f;
    [SerializeField] private float minPowerUpDistance = 50f;
    [SerializeField] private Text _score;
    // Available pickup variants and the seeded first instance used as the generation anchor.
    [SerializeField] private PowerUp[] powerUpPrefab;

    [SerializeField] private int powerUpCount = 5;
    [SerializeField] private float maxWeight = 2.5f;

    [SerializeField] private PowerUp firstPowerUp;

    private PowerUp lastPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        // Generation is chained from the known first pickup, so new objects continue upward from it.
        lastPowerUp = firstPowerUp;

        if (lastPowerUp == null)
        {
            lastPowerUp = CreatePowerUp(transform.position);
            if (lastPowerUp == null)
            {
                Debug.LogError("PowerUpManager needs at least one PowerUp prefab assigned.", this);
                enabled = false;
                return;
            }
        }

        GeneratePowerUp();
    }

    void GeneratePowerUp()
    {
        // Keep updating lastPowerUp so each following item is placed above the preceding one.
        for (int i = 0; i < powerUpCount; i++)
        {
            Vector3 position = GetNextPos();

            lastPowerUp = CreatePowerUp(position);
            if (lastPowerUp == null)
            {
                Debug.LogError("PowerUpManager could not find a valid PowerUp prefab.", this);
                enabled = false;
                return;
            }
            lastPowerUp.SetOriginPosition(position);
        }
    }

    private PowerUp CreatePowerUp(Vector3 position)
    {
        if (powerUpPrefab == null || powerUpPrefab.Length == 0)
            return null;

        for (int attempt = 0; attempt < powerUpPrefab.Length; attempt++)
        {
            PowerUp prefab = powerUpPrefab[Random.Range(0, powerUpPrefab.Length)];
            if (prefab != null)
                return Instantiate(prefab, position, Quaternion.identity);
        }

        return null;
    }


    Vector3 GetNextPos()
    {
        // Difficulty scales with the score by widening the vertical separation over time.
        minPowerUpDistance += float.Parse(_score.text) * 1 / 100;
        maxPowerUpDistance += float.Parse(_score.text) * 1 / 100;
        Vector3 pos = new Vector3(Random.Range(-maxWeight, maxWeight),
            lastPowerUp.transform.position.y + Random.Range(minPowerUpDistance, maxPowerUpDistance), 0);
        return pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Crossing this cleanup trigger removes an old pickup and replaces one at the top of the chain.
        if (collision.gameObject.tag == "PowerUP")
        {
            // The collider belongs to the pickup, so remove its whole GameObject.
            Destroy(collision.gameObject);
            powerUpCount = 1;
            GeneratePowerUp();
            /*MoveUpEnemy(collision.gameObject.GetComponent<Enemy>());*/
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
