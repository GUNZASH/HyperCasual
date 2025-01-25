using UnityEngine;

public class PowerUpBullet : MonoBehaviour
{
    public Shoot shootScript;
    public float powerUpDuration = 7f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shootScript.ActivatePowerUp(powerUpDuration);
            Destroy(gameObject);
        }
    }
}