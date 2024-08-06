using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatedController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // anim.SetInteger("animState", 1);

        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetInteger("animState", 1);
            } else if (horizontalInput != 0 || verticalInput != 0)
            {
                anim.SetInteger("animState", 2); 
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetInteger("animState", 3); 
                }
            } else if (Input.GetMouseButtonDown(0))
            {
                anim.SetInteger("animState", 4);
            } else
            {
                anim.SetInteger("animState", 0);
            }
            
        }
    }
}
