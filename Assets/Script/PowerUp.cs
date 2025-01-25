using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fireRateIncrease = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<Shoot>().IncreaseFireRate(fireRateIncrease);

            Destroy(gameObject);
        }
    }
}