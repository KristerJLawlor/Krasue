using UnityEngine;

public class InputManager : MonoBehaviour
{
    //make the InputManager a singleton
    private static InputManager _instance;

    public static InputManager Instance { 
        get {
            return _instance; } 
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        //make sure there is only one instance of the InputManager
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControls = new PlayerControls();
        Cursor.visible = false; //hide the cursor
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    //helper functions

    public Vector2 GetMovementInput()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetLookInput()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool GetInteractInput()
    {
        return playerControls.Player.Interact.triggered;
    }

    public bool GetLightInput()
    {
        return playerControls.Player.Light.triggered;
    }

    public bool IsSprinting()
    {
        return playerControls.Player.Sprint.IsInProgress();
    }

    public bool IsCrouching()
    {
        return playerControls.Player.Crouch.IsInProgress();
    }




}
