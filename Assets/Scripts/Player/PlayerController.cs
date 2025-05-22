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
    private float playerSpeed = 0f;
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

    //Animation
    private Animator animator;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;   //store the reference for input manager instance
        cameraTransform = Camera.main.transform; //get the main camera

        // For Camera Crouch
        headObject = this.transform.GetChild(0).gameObject; //Cinemachine is tied to the Follow Me object
        cameraStandPos = headObject.transform.localPosition;
        cameraCrouchPos = cameraStandPos + new Vector3(0, crouchCameraOffset, 0);
        originalHeight = controller.height;
        canMove = true;

        // For animation
        animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
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
            //UnityEngine.Debug.Log("Final Move: " + finalMove);
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


        // Animation
        if(!inputManager.IsMoving() && !inputManager.IsCrouching())
        {
            //Idle
            animator.SetFloat("Speed", 0);
            animator.SetBool("isCrouching", false);
            //UnityEngine.Debug.Log("Idle");
        }
        //else if (!inputManager.IsMoving() && inputManager.IsCrouching())
        //{
        //    //Crouch Idle
        //    animator.SetFloat("Speed", 0);
        //    animator.SetBool("isCrouching", true);
        //    UnityEngine.Debug.Log("Idle Crouch");
        //}
        else if(inputManager.IsMoving() && !inputManager.IsSprinting() && !inputManager.IsCrouching())
        {
            //Walking
            animator.SetFloat("Speed", 4f);
            animator.SetBool("isCrouching", false);
            //UnityEngine.Debug.Log("walking");
        }
        //else if (inputManager.IsMoving() && !inputManager.IsSprinting() && isCrouching)
        //{
        //    //Crouch Walking
        //    animator.SetFloat("Speed", 1f);
        //    animator.SetBool("isCrouching", true);
        //    UnityEngine.Debug.Log("crouch walk");
        //}
        else if (inputManager.IsMoving() && inputManager.IsSprinting() && !isCrouching)
        {
            //Running
            animator.SetFloat("Speed", 8);
            animator.SetBool("isCrouching", false);
            //UnityEngine.Debug.Log("running");
        }

    }


}


