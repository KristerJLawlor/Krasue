using UnityEngine;

public class CrouchBehavior : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerController playerController;
    private bool isCrouching = false;

    void Start()
    {
        inputManager = InputManager.Instance;   //store the reference for input manager instance
        playerController = GetComponentInParent<PlayerController>(); //get the player controller component
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.IsCrouching())
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }


    }
}
