using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GeneratePowerUp();
    }

    void GeneratePowerUp()
    {
        // Keep updating lastPowerUp so each following item is placed above the preceding one.
        for (int i = 0; i < powerUpCount; i++)
        {
            Vector3 position = GetNextPos();

            PowerUp powerUp = powerUpPrefab[Random.Range(0, powerUpPrefab.Length)];
            lastPowerUp = Instantiate(powerUp, position, Quaternion.identity);
            lastPowerUp.SetOriginPosition(position);
        }
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
            Destroy(collision);
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
