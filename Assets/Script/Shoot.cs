using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public Transform[] additionalShootPoints;
    private int shootPointCount = 1;

    public float shootingSpeed = 0.5f;

    private bool isPowerUpActive = false;
    private float powerUpStartTime;
    private float powerUpDuration;

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the shoot sound to the AudioSource
        audioSource.clip = shootSound;
    }
    private void Update()
    {
        if (isPowerUpActive && Time.time - powerUpStartTime > powerUpDuration)
        {
            isPowerUpActive = false;
            shootPointCount = 1;

            if (additionalShootPoints.Length > 1)
            {
                Destroy(additionalShootPoints[1].gameObject);
                Destroy(additionalShootPoints[2].gameObject);
                additionalShootPoints = new Transform[1];
                additionalShootPoints[0] = shootPoint;
            }
        }

        if (Time.time > nextFireTime)
        {
            Shot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shot()
    {
        for (int i = 0; i < shootPointCount; i++)
        {
            if (additionalShootPoints[i] != null)
            {
                Instantiate(bulletPrefab, additionalShootPoints[i].position, additionalShootPoints[i].rotation);
            }
        }

        audioSource.PlayOneShot(shootSound);
    }


    public void IncreaseFireRate(float amount)
    {
        fireRate -= amount;
        if (fireRate < 0.1f)
        {
            fireRate = 0.1f;
        }

    }

    private void IncreaseShootingPoints()
    {
        //เพิ่มจุดยิงจาก 1 เป็นยิงทีละ 3 จะทำเพิ่่มอีกก็ได้
        shootPointCount = 3;

        Transform leftShootPoint = new GameObject("LeftShootPoint").transform;
        Transform rightShootPoint = new GameObject("RightShootPoint").transform;

        leftShootPoint.position = shootPoint.position + new Vector3(-0.5f, 0.5f, 0);
        rightShootPoint.position = shootPoint.position + new Vector3(0.5f, 0.5f, 0);

        leftShootPoint.rotation = shootPoint.rotation;
        rightShootPoint.rotation = shootPoint.rotation;


        leftShootPoint.SetParent(transform);
        rightShootPoint.SetParent(transform);


        additionalShootPoints = new Transform[3];
        additionalShootPoints[0] = shootPoint;
        additionalShootPoints[1] = leftShootPoint;
        additionalShootPoints[2] = rightShootPoint;


    }
    public void ActivatePowerUp(float duration)
    {
        isPowerUpActive = true;
        powerUpStartTime = Time.time;
        IncreaseShootingPoints();


        powerUpDuration = duration;
    }
}