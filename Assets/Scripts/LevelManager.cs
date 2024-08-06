using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 60.0f;
    public static bool isGameStarted = false;
    public static bool isGameOver = false;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    // public AudioClip teleportSFX;
    public Text timeText;
    public Text gameText;
    public Text powerUpText;
    public int attackPower = 10;
    public float fireRate = 0.5f;
    float countDown;
    int powerupCount;
    public GameObject[] enemies; // References to the enemy GameObjects
    public GameObject crosshairUI; // Reference to the Crosshair UI element
    public GameObject timerUI; // Reference to the Timer UI element
    public GameObject NPC; 
    public PlayerController playerController;
    public ShootProjectile shootProjectile;
    public string nextLevel;

    void Start()
    {
        playerController.enabled = true; // Enable player control
        shootProjectile.enabled = true;
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true); 
            }
        }
        crosshairUI.SetActive(true); // Show Crosshair UI
        timerUI.SetActive(true); // Show Timer UI
        NPC.SetActive(false);

        isGameOver = false;
        countDown = levelDuration;
        ShowPowerupNumber();
    }

    // Update is called once per frame
    void Update()
    {
        ShowPowerupNumber();
        if (!isGameOver)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = 0.0f;
                LevelLose();
            }
            timeText.text = countDown.ToString("F1");
        }
    }

    void ShowPowerupNumber()
    {
        powerupCount = GameObject.FindWithTag("Player").GetComponent<PlayerController>().attackLootNumber;
        powerUpText.text = "Power-up: " + powerupCount.ToString();
    }

    // A player has to destroy all enemies and arrive at the exit to win
    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "Level Completed!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        // AudioSource.PlayClipAtPoint(teleportSFX, Camera.main.transform.position);
        Invoke("LoadNextLevel", 2);
    }

    public void LevelLose()
    {
        isGameOver = true;
        gameText.text = "You Died!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("ReloadScene", 2);
    }

    void LoadNextLevel()
    {
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
