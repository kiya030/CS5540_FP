using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHit : MonoBehaviour
{
    public GameObject healthLootPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);

            Instantiate(healthLootPrefab, transform.position, Quaternion.identity);
            // One projectile can only shoot one enemy
            Destroy(other);
        }
    }
}
