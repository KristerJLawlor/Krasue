using UnityEditor.Rendering.Canvas.ShaderGraph;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [SerializeField] private float walkingBobbingSpeed = 10f;
    [SerializeField] private float sprintingBobbingSpeed = 16f;
    [SerializeField] private float bobbingAmountWalking = 0.2f;
    [SerializeField] private float bobbingAmountSprinting = 0.35f;

    private PlayerController controller;
    private InputManager inputManager;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<PlayerController>(); //get the player controller component
        inputManager = InputManager.Instance;   //store the reference for input manager instance
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((controller.canMove && !inputManager.IsCrouching()))
        {
            
            if (Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f)
            {
                if (!inputManager.IsSprinting())
                {                    
                    //Walking
                    timer += Time.deltaTime * walkingBobbingSpeed;
                    transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmountWalking, transform.localPosition.z);
                }
                else
                {
                    //Sprinting
                    timer += Time.deltaTime * sprintingBobbingSpeed;
                    transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmountSprinting, transform.localPosition.z);
                }

            }
            else
            {
                //Idle
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
            }           
        }

    }
}
