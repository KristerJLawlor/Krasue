using Unity.Cinemachine;
using UnityEngine;

public class ImpulseCinemachine : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] float bobSpeed;

    public void ScreenBob(float bobSpeed)
    {
            impulseSource.GenerateImpulse(Vector3.up * bobSpeed);
        
    }

}
