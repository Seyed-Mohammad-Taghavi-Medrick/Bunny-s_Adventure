using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script: Generates enemies above the player path and replaces enemies that leave the play area.
   Cheat sheet: Random.Range selects spawn positions and prefabs; Destroy removes a GameObject; a for loop repeats generation. */

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

        if (lastEnemy == null)
        {
            lastEnemy = CreateEnemy(transform.position);
            if (lastEnemy == null)
            {
                Debug.LogError("EnemyManager needs at least one Enemy prefab assigned.", this);
                enabled = false;
                return;
            }
        }

        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        // Successive spawn positions are relative to lastEnemy, creating an ascending sequence.
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 position = GetNextPos();

            lastEnemy = CreateEnemy(position);
            if (lastEnemy == null)
            {
                Debug.LogError("EnemyManager could not find a valid Enemy prefab.", this);
                enabled = false;
                return;
            }
            lastEnemy.SetOriginPosition(position);
        }
    }

    private Enemy CreateEnemy(Vector3 position)
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            return null;

        for (int attempt = 0; attempt < enemyPrefabs.Length; attempt++)
        {
            Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            if (prefab != null)
                return Instantiate(prefab, position, Quaternion.identity);
        }

        return null;
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
            // The collider belongs to the enemy, so remove its whole GameObject.
            Destroy(collision.gameObject);
            enemyCount = 1;
            GenerateEnemies();
            /*MoveUpEnemy(collision.gameObject.GetComponent<Enemy>());*/
        }
    }
}
