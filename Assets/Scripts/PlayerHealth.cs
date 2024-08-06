// Player health is influenced by enemy attack and health loots
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image heartIcon;
    public bool isDead;
    int currentHealth;
    float damageTimer = 0f;
    public AudioClip playerGruntSFX;
    private bool isCollidingWithEnemy = false;
    private int enemyDamage = 0;

    void Start()
    {
        currentHealth = startingHealth;
        isDead = false;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollidingWithEnemy)
        {
           
            damageTimer += Time.deltaTime;

            // Apply damage at one-second intervals
            if (damageTimer >= 1f)
            {
                AudioSource.PlayClipAtPoint(playerGruntSFX, Camera.main.transform.position); // Play grunt sound each second
                ApplyDamage();
                damageTimer -= 1f; // Decrement the timer by one second
            }
        }
    }

    void PlayerDies()
    {
        if (!isDead)
        {
            isDead = true;
            currentHealth = 0;
            transform.Rotate(-90, 0, 0, Space.Self);
            heartIcon.color = Color.gray;
            FindObjectOfType<LevelManager>().GetComponent<LevelManager>().LevelLose();
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        // Health loots increase player's health
        if (other.CompareTag("HealthLoot"))
        {
            if (currentHealth <= startingHealth - 10)
            {
                currentHealth += 10;
            }
            else
            {
                currentHealth = startingHealth;
            }
            healthSlider.value = currentHealth;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = true;
            enemyDamage = collision.collider.GetComponent<EnemyBehavior>().damagePerSecond;
            AudioSource.PlayClipAtPoint(playerGruntSFX, Camera.main.transform.position);
            ApplyDamage();
            damageTimer = 0f; // Set timer to -1 to indicate initial damage done
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = false;
            damageTimer = 0;
        }
    }

    void ApplyDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth -= enemyDamage;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }
}
