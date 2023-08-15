using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float maxPowerUpDistance = 100f;
    [SerializeField] private float minPowerUpDistance = 50f;
    [SerializeField] private Text _score;
    [SerializeField] private PowerUp[] powerUpPrefab;

    [SerializeField] private int powerUpCount = 5;
    [SerializeField] private float maxWeight = 2.5f;

    [SerializeField] private PowerUp firstPowerUp;

    private PowerUp lastPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        lastPowerUp = firstPowerUp;
        GeneratePowerUp();
    }

    void GeneratePowerUp()
    {
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
        minPowerUpDistance += float.Parse(_score.text) * 1 / 100;
        maxPowerUpDistance += float.Parse(_score.text) * 1 / 100;
        Vector3 pos = new Vector3(Random.Range(-maxWeight, maxWeight),
            lastPowerUp.transform.position.y + Random.Range(minPowerUpDistance, maxPowerUpDistance), 0);
        return pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
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