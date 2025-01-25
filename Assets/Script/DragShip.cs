using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragShip : MonoBehaviour
{
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -4f;
    public float maxY = 4f;

    public float moveSpeed = 10f;

    public int health = 3;
    public int maxHealth = 3;
    public Image[] heartIcons;

    private bool isShieldActive = false;
    public float shieldDuration = 5f;

    public GameObject shieldObject;


    public Animator playerAnimator;
    public AudioClip deathSound;
    private AudioSource audioSource;

    public TextMeshProUGUI gameOverText;
    public Image gameOverImage;
    public GameObject gameOverPanel;
    void Start()
    {
        // UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        gameOverText.enabled = false;
        gameOverImage.enabled = false;

        // Shield
        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (health > 0 && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            touchPosition.x = Mathf.Clamp(touchPosition.x, minX, maxX);
            touchPosition.y = Mathf.Clamp(touchPosition.y, minY, maxY);
            touchPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, touchPosition, Time.deltaTime * moveSpeed);
        }
    }

    public void TakeDamage()
    {
        if (isShieldActive) return;

        health--;
        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = i < health;
        }
    }

    private void Die()
    {
        playerAnimator.SetTrigger("isDead");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        StartCoroutine(ShowGameOverAfterDelay());

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        playerAnimator.SetTrigger("isDead");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        StartCoroutine(ShowGameOverAfterDelay());
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("Player Died!");
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        gameOverText.enabled = true;
        gameOverImage.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isShieldActive)
            {
                Destroy(other.gameObject);
            }
            else
            {
                TakeDamage();
            }
        }

        if (other.gameObject.CompareTag("Healing"))
        {
            Heal(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            ActivateShield();
            Destroy(other.gameObject);
        }
    }

    private void ActivateShield()
    {
        if (isShieldActive) return;

        isShieldActive = true;
        Debug.Log("Shield Activated!");

        if (shieldObject != null)
        {
            shieldObject.SetActive(true);
            shieldObject.GetComponent<Collider2D>().enabled = true;
        }

        StartCoroutine(ShieldDuration());
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);

        isShieldActive = false;
        Debug.Log("Shield Deactivated!");

        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
            shieldObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
