using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    public FSMStates currentState;
    public Transform player;
    public GameObject healthLootPrefab;
    public GameObject enemyDefeated;
    public GameObject enemyAttacked;
    public AudioClip enemyGrunt;
    public Slider healthSlider;
    public float moveSpeed = 5;
    public float leaveSpeed = 1;
    public float minDistance = 1;
    public int damagePerSecond = 20;
    public static int enemyCount = 0;

    public int enemyHealth = 50;
    int currentHealth;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyCount++;
        currentHealth = enemyHealth;
        healthSlider.value = currentHealth;
    }

    // Enemies move towards player when player is alive, leave them otherwise
    void Update()
    {
        if (player.GetComponent<PlayerHealth>().isDead)
        {
            LeavePlayer();
        }
        else
        {
            // Enemies face and move toward the player
            float step = moveSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > minDistance)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
        }
    }

    // Enemies leave player when the player is dead
    void LeavePlayer()
    {
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        directionAwayFromPlayer.y = 0;

        Vector3 moveDirection = leaveSpeed * Time.deltaTime * directionAwayFromPlayer.normalized;

        transform.position += moveDirection;
    }

    void EnemyDefeated()
    {
        Instantiate(enemyDefeated, transform.position, transform.rotation);

        gameObject.SetActive(false);
        enemyCount--;
        AudioSource.PlayClipAtPoint(enemyGrunt, Camera.main.transform.position);
        Destroy(gameObject, 0.5f);

        Instantiate(healthLootPrefab, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Instantiate(enemyAttacked, transform.position, transform.rotation);
            currentHealth -= FindObjectOfType<LevelManager>().attackPower;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                EnemyDefeated();
            }
            Destroy(other);
        }
    }
}
