// Shot Projectiles will be destroyed after 3 seconds
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    public float destroyDuration = 3;

    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
