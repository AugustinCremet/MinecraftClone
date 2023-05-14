using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] Mining mining;

    PlayerControl control;
    PlayerControl.PlayerMovementActions playerMovement;
    PlayerControl.PlayerMiningActions playerMining;

    Vector2 moveInput;
    Vector2 mouseInput;
    float leftClick;
    float runClick;

    void Awake()
    {
        control = new PlayerControl();
        playerMovement = control.PlayerMovement;
        playerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Jump.performed += _ => movement.OnJumpPressed();
        playerMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        playerMovement.Run.performed += ctx => runClick = ctx.ReadValue<float>();

        playerMining = control.PlayerMining;
        playerMining.LeftClick.performed += ctx => leftClick = ctx.ReadValue<float>();
    }

    void OnEnable()
    {
        control.Enable();
    }

    void OnDisable()
    {
        control.Disable();
    }

    void Update()
    {
        movement.ReceiveInput(moveInput);
        cameraMovement.ReceiveInput(mouseInput);
        mining.OnLeftClickPressed(leftClick);
        movement.OnRunPressed(runClick);
    }
}
