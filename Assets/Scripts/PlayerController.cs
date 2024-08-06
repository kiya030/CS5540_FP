// This script makes it possible to control the player's movement with keyboard input.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public float airControl = 10;

    // public int attackPower = 10;
    public float powerUpDuration = 5f;
    public int attackLootNumber = 1;
    public AudioClip jumpSFX;

    CharacterController controller;
    Vector3 moveDirection, input;
    float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            currentSpeed = moveSpeed;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= 2;
            }

            input *= currentSpeed;

            if (controller.isGrounded)
            {
                moveDirection = input;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                    // AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
                }
                else
                {
                    moveDirection.y = 0.0f;
                }
            }
            else
            {
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            }

            // Simulates the gravitational force
            moveDirection.y -= gravity * Time.deltaTime;

            controller.Move(Time.deltaTime * moveDirection);

            // Attack power increases for a short period of time if a loot is used
            if (attackLootNumber > 0 && Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(IncreaseAttackPower());
            }
        }        
    }

    private IEnumerator IncreaseAttackPower()
    {
        attackLootNumber--;
        int originalAttackPower = FindObjectOfType<LevelManager>().attackPower;
        FindObjectOfType<LevelManager>().attackPower *= 2;
        yield return new WaitForSeconds(powerUpDuration);
        FindObjectOfType<LevelManager>().attackPower = originalAttackPower;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Exit") && !LevelManager.isGameOver)
        {
            if (EnemyBehavior.enemyCount == 0)
            {
                FindObjectOfType<LevelManager>().GetComponent<LevelManager>().LevelBeat();
            }
        }
    }
}
