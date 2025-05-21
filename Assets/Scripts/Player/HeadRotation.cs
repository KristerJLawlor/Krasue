using UnityEngine;

public class HeadLookAtCamera : MonoBehaviour
{
    public Transform playerHead;
    public float minClamp = -45f;
    public float maxClamp = 45f;

    void Update()
    {

    }

    private void LateUpdate()
    {
        // Get the camera's rotation
        Quaternion cameraRotation = Camera.main.transform.rotation;
        Debug.Log("Camera Rotation: " + cameraRotation.eulerAngles);
        // Apply the rotation to the head
        playerHead.transform.rotation = cameraRotation;

        // Clamp the head's rotation (optional)
        Vector3 localHeadRotation = playerHead.transform.localRotation.eulerAngles;
        localHeadRotation.y = Mathf.Clamp(localHeadRotation.y, minClamp, maxClamp);
        playerHead.transform.localRotation = Quaternion.Euler(localHeadRotation);
        Debug.Log("Head Rotation: " + playerHead.transform.rotation);
    }
}
