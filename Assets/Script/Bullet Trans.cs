using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrans : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletDestroyArea"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHP>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}