using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Generates enemies above the current stack and replaces those that cross its cleanup trigger.</summary>
public class EnemyManager : MonoBehaviour
{
    // Vertical spacing and horizontal range. Spacing tightens as score increases.
    [SerializeField] private float maxEnemyDistance = 100f;
    [SerializeField] private float minEnemyDistance = 50f;
    [SerializeField] private Text _score;

    [SerializeField] private Enemy[] enemyPrefabs;

    /*[SerializeField] private float enemyDistance = 25;*/
    [SerializeField] int enemyCount = 5;
    [SerializeField] private float maxWeight = 3.5f;

    [SerializeField] Enemy firstEnemy;
    Enemy lastEnemy;

    void Start()
    {
        // Anchor the first generated enemy to the scene-placed seed object.
        lastEnemy = firstEnemy;
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        // Successive spawn positions are relative to lastEnemy, creating an ascending sequence.
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
        // Score progression decreases gaps, increasing encounter density at higher altitude.
        minEnemyDistance = minEnemyDistance - float.Parse(_score.text) * 1 / 100;
        maxEnemyDistance = maxEnemyDistance - float.Parse(_score.text) * 1 / 100;
        Vector3 pos = new Vector3(Random.Range(-maxWeight, maxWeight),
            lastEnemy.transform.position.y + Random.Range(minEnemyDistance, maxEnemyDistance), 0);
        return pos;
    }

    public void MoveUpEnemy(Enemy enemy)
    {
        // Legacy recycle path: relocate an existing enemy and make it the next placement anchor.
        enemy.transform.position = GetNextPos();
        lastEnemy = enemy;
        enemy.SetOriginPosition(enemy.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The active path destroys off-screen enemies and spawns one replacement rather than reusing it.
        if (collision.gameObject.tag == "Enemy")
        {
            // Collider بخشی از دشمن است؛ باید کل GameObject حذف شود.
            Destroy(collision.gameObject);
            enemyCount = 1;
            GenerateEnemies();
            /*MoveUpEnemy(collision.gameObject.GetComponent<Enemy>());*/
        }
    }
}
