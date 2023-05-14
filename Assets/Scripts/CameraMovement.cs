using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float verticalSpeed = 1f;
    float mouseX;
    float mouseY;

    [SerializeField] Transform playerCam;
    [SerializeField] float xClamp = 85.0f;
    float xRotation = 0.0f;

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * horizontalSpeed;
        mouseY = mouseInput.y * verticalSpeed;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCam.eulerAngles = targetRotation;
    }
}
