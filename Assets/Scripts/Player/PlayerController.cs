using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTransform;
    private CharacterController controller;

    // Player camera variables
    private GameObject headObject;

    // Player movement variables
    private float playerSpeed = 4.0f;
    private float walkSpeed = 4.0f;
    private float sprintSpeed = 8.0f;     
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;   
    public Vector3 moveDirection;
    private Vector3 bodyForward;
    private bool groundedPlayer;
    public bool canMove;    //check if player can move or not. This is used for cutscenes and other situations where we don't want the player to move

    // Crouch specific variables
    private Vector3 cameraStandPos;
    private Vector3 cameraCrouchPos;    
    private float crouchSpeed = 1.0f; 
    private float crouchHeight = 0.5f;
    private float originalHeight;
    private float crouchTransitionSpeed = 6.0f;
    private float crouchCameraOffset = -1f;
    public bool isCrouching = false;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;   //store the reference for input manager instance
        cameraTransform = Camera.main.transform; //get the main camera
        //cinemachineCamera;

        // For Camera Crouch
        headObject = this.transform.GetChild(0).gameObject; //Cinemachine is tied to the Follow Me object
        cameraStandPos = headObject.transform.localPosition;
        cameraCrouchPos = cameraStandPos + new Vector3(0, crouchCameraOffset, 0);
        originalHeight = controller.height;
        canMove = true;

        
    }

    void Update()
    {
        UnityEngine.Debug.Log("playerSpeed: " + playerSpeed);
        //UnityEngine.Debug.Log("main camera vector3: " + cameraTransform.transform.forward);
        PlayerController playerController = GetComponent<PlayerController>();
        playerController.controller.height = 0;
        // Check if player is jumping
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
        if (inputManager.PlayerJumped() && groundedPlayer && canMove)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        
        // Crouch
        if (inputManager.IsCrouching() && groundedPlayer && canMove)
        {
            isCrouching = true; 
            controller.height = crouchHeight;
            playerSpeed = crouchSpeed;


        }
        else
        {
            isCrouching = false;
            controller.height = originalHeight;
            playerSpeed = walkSpeed;

        }

        // Sprint
        if (inputManager.IsSprinting() && !inputManager.IsCrouching() && canMove)
        {
            playerSpeed = sprintSpeed; //set the player speed to sprinting speed
        }
        else if(!inputManager.IsSprinting() && !inputManager.IsCrouching() && canMove)
        {
            playerSpeed = walkSpeed; //set the player speed to normal speed
        }

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        if (canMove)
        {
            UnityEngine.Debug.Log("Final Move: " + finalMove);
            controller.Move(finalMove * Time.deltaTime);
        }
        

        moveDirection = finalMove; //store the final move direction for other scripts to use
    }

    private void LateUpdate()
    {
        if (inputManager.IsCrouching() && groundedPlayer && canMove)
        {
            //interpolate the camera position to the crouch position via Follow Me object
            headObject.transform.localPosition = Vector3.Lerp(headObject.transform.localPosition, cameraCrouchPos,
                                                              Time.deltaTime * crouchTransitionSpeed);
        }
        else
        {
            //Lerp the camera position to the stand position via Follow Me object
            headObject.transform.localPosition = Vector3.Lerp(headObject.transform.localPosition, cameraStandPos,
                                                              Time.deltaTime * crouchTransitionSpeed);
        }

        // Rotate the player to face the camera direction
        bodyForward = cameraTransform.transform.forward;
        bodyForward.y = 0; // Set y to 0 to ignore vertical rotation
        bodyForward.Normalize(); // Normalize the vector to ensure consistent rotation
        Quaternion targetRotation = Quaternion.LookRotation(bodyForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerSpeed);    //make player rotate quicker if running or slower if crouching

    }

}


