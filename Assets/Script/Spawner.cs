using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] spawnChances = { 0.45f, 0.45f, 0.1f };
    public float initialSpawnInterval = 5f;
    public float minSpawnInterval = 1f;
    public float spawnIntervalDecrement = 0.2f;
    public int scoreThreshold = 50;
    public BoxCollider2D spawnArea;
    public float gravityScaleIncreaseRate = 0.02f;
    public float maxGravityScale = 1f;
    private float gravityScale;

    private float currentSpawnInterval;
    private int lastScoreCheckpoint = 0;
    private ScoreManager scoreManager;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        gravityScale = 0.1f;
        scoreManager = FindObjectOfType<ScoreManager>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            UpdateSpawnInterval();

            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                spawnArea.bounds.max.y
            );

            GameObject enemyPrefab = GetRandomEnemy();

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = gravityScale;
            }

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    private GameObject GetRandomEnemy()
    {
        float randomValue = Random.value;
        float cumulativeChance = 0f;

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            cumulativeChance += spawnChances[i];
            if (randomValue <= cumulativeChance)
            {
                return enemyPrefabs[i];
            }
        }

        return enemyPrefabs[0];
    }

    private void UpdateSpawnInterval()
    {
        int currentScore = scoreManager.score;

        if (currentScore >= lastScoreCheckpoint + scoreThreshold)
        {
            gravityScale = Mathf.Min(gravityScale + gravityScaleIncreaseRate, maxGravityScale);

            if (currentScore >= 1000)
            {
                currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecrement, minSpawnInterval);
            }

            lastScoreCheckpoint += scoreThreshold;
        }
    }
}