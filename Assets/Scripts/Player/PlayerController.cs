using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTransform;
    private CharacterController controller;

    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;   
    public Vector3 moveDirection;
    private bool groundedPlayer;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;   //store the reference for input manager instance
        cameraTransform = Camera.main.transform; //get the main camera transform
    }

    void Update()
    {   
        //check if player is jumping or not. If not, set y axis velocity to 0, so we don't fall through the ground
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Horizontal input
        Vector3 movement = inputManager.GetMovementInput();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x; //get the camera direction and apply it to the movement vector
        move.y = 0; //set y axis to 0 so we don't move up or down when moving

        // Jump Input
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Sprint
        if(inputManager.IsSprinting())
        {
            playerSpeed = 8.0f; //set the player speed to sprinting speed
        }
        else
        {
            playerSpeed = 4.0f; //set the player speed to normal speed
        }

        // Crouch
        if (inputManager.IsCrouching())
        {
        }
        else
        {
        }

            // Combine horizontal and vertical movement
            Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);

        moveDirection = finalMove; //store the final move direction for other scripts to use
    }
}
