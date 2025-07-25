using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;  // Reference to the player transform
    public Vector3 offset;             // Offset from the player position
    public Vector3 alternatePosition;  // Alternate position for the camera to move to
    private bool followPlayer = true;  // Whether the camera is following the player or not

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // When the 'E' key is pressed
        {
            followPlayer = !followPlayer; // Toggle between following the player and the alternate position
        }
    }

    private void LateUpdate()
    {
        if (followPlayer)
        {
            transform.position = playerTransform.position + offset; // Follow the player with offset
        }
        else
        {
            transform.position = alternatePosition; // Move the camera to the alternate position
        }
    }
}
