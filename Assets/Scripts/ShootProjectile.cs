using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100;
    public Image reticalImage;
    public Color reticalEnemyColor;

    Color originalReticalColor;

    private float nextFireTime;

    public float attackRange = 50;
    public AudioClip pistolSFX;

    void Start()
    {
        originalReticalColor = reticalImage.color;
        nextFireTime = 0f;
    }

    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + FindObjectOfType<LevelManager>().fireRate;
                // Instantiate the projectile in front of the camera
                GameObject projectile = Instantiate(
                    projectilePrefab, transform.position + transform.forward, transform.rotation);

                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
                AudioSource.PlayClipAtPoint(pistolSFX, Camera.main.transform.position);

                // Store all instantiated projectiles into ProjectileParent folder
                projectile.transform.SetParent(
                    GameObject.FindGameObjectWithTag("ProjectileParent").transform);
            }
        } 
    }

    private void FixedUpdate()
    {
        ReticalEffect();
    }

    // Modify the crosshair when aiming at an enemy
    void ReticalEffect()
    {
        RaycastHit hit;
        // See if player is aiming at an enemy
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            // If so, the crosshair will change color and become smaller
            if (hit.collider.CompareTag("Enemy"))
            {
                reticalImage.color = Color.Lerp(
                    reticalImage.color, reticalEnemyColor, Time.deltaTime * 2);
                reticalImage.transform.localScale = Vector3.Lerp(
                    reticalImage.transform.localScale, new Vector3(0.7f, 0.7f, 1),
                    Time.deltaTime * 2);
            }
            // Crosshair returns to the original setting otherwise
            else
            {
                reticalImage.color = Color.Lerp(
                    reticalImage.color, originalReticalColor, Time.deltaTime * 2);
                reticalImage.transform.localScale = Vector3.Lerp(
                    reticalImage.transform.localScale, Vector3.one,
                    Time.deltaTime * 2);
            }
        }
    }
}
