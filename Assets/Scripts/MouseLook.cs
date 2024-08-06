// This script is used to control player's vision through mouse movement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;

    // To be implemented: modify sensitivity in the setting
    public float mouseSensitivity = 10;

    float pitch = 0;

    void Start()
    {
        // Get access to the player (parent object of camera)
        playerBody = transform.parent.transform;

        // Cursor is locked in the middle of the screen and invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Player rotates around the y axis
        playerBody.Rotate(Vector3.up * moveX);

        // Camera rotates around the x axis
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -90f, 90f); // Player looks 90 degrees up and down at most
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
