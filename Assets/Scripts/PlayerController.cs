using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity; // player velocity
    private bool groundedPlayer; // return if player is grounded
    private Transform cameraTranform;
    private PlayerInput playerInput;

    private InputAction movementAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTranform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = movementAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x,0f,input.y);
        move = cameraTranform.forward * move.z + cameraTranform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
