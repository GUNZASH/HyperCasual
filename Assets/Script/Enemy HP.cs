using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int hitPoints = 3;
    public int maxHitPoints = 3;
    public int scoreValue = 1;

    public GameObject powerUpPrefab;
    public GameObject healingPrefab;
    public GameObject shieldPrefab;

    public float powerUpChance = 0.4f;
    public float healingChance = 0.4f;
    public float shieldChance = 0.2f;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            scoreManager.AddScore(scoreValue);

            DropItem();

            Destroy(gameObject);
        }
    }


    void DropItem()
    {
        float randomValue = Random.value;

        if (randomValue <= powerUpChance)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue <= powerUpChance + healingChance)
        {
            Instantiate(healingPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue <= powerUpChance + healingChance + shieldChance)
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}