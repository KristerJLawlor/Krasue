using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    InputManager inputManager;
    float interactRange = 2f;
    Collider[] colliderArray = null;
    void Start()
    {
        inputManager = InputManager.Instance; //store the reference for input manager instance
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.InteractTriggered())
        {
            colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteract npcInteract))
                {
                    npcInteract.Interact();
                }
            }
        }

    }
}
