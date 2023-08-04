using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private float maxEnemyDistance= 100f;
    [SerializeField] private float minEnemyDistance= 50f;
    [SerializeField] private Text _score;
    [SerializeField] private Enemy[] enemyPrefabs;
    /*[SerializeField] private float enemyDistance = 25;*/
    [SerializeField] int enemyCount = 5;
    [SerializeField] private float maxWeight = 3.5f;

    [SerializeField] Enemy firstEnemy;
    Enemy lastEnemy;

    void Start()
    {
        lastEnemy = firstEnemy;
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 position = GetNextPos();

            Enemy enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            lastEnemy = Instantiate(enemy, position, Quaternion.identity);
            lastEnemy.SetOriginPosition(position);
        }
    }

    Vector3 GetNextPos()
    {
        minEnemyDistance -= float.Parse(_score.text) * 1 / 100;
        maxEnemyDistance -= float.Parse(_score.text) * 1 / 100;
        Vector3 pos = new Vector3(Random.Range(-maxWeight,maxWeight), lastEnemy.transform.position.y + Random.Range(minEnemyDistance,maxEnemyDistance), 0);
        return pos;
    }

    public void MoveUpEnemy(Enemy enemy)
    {
        enemy.transform.position = GetNextPos();
        lastEnemy = enemy;
        enemy.SetOriginPosition(enemy.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision);
            enemyCount = 1;
            GenerateEnemies();
            /*MoveUpEnemy(collision.gameObject.GetComponent<Enemy>());*/
        }
    }
}