using Unity.Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    // This is a custom extension for Cinemachine that allows for a point of view (POV) camera system.
    // It can be used to create a first-person or third-person camera system with smooth transitions and controls.
    // Add any necessary variables or properties here
    // This method is called before the camera is updated

    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        base.Awake();
        inputManager = InputManager.Instance; // Store the reference for input manager instance
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                //get the mouse input from the input manager reference
                Vector2 deltaInput = inputManager.GetLookInput();
                //update the starting rotation based on the mouse input
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                //Clamp the rotation angles to prevent flipping from camera rotation
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                //need to set y value to negative so it isnt inverted
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
