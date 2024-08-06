// Once a health loot is triggered, it disappears
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public AudioClip lootUsedSFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("supplyPickedup");
            AudioSource.PlayClipAtPoint(lootUsedSFX, Camera.main.transform.position);
            Destroy(gameObject, 1);
        }
    }
}
