using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    InputManager inputManager;
    float interactRange = 3f;

    void Start()
    {
        inputManager = InputManager.Instance; //store the reference for input manager instance
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.InteractTriggered())
        {
            IInteract interactableObject = GetInteractableObject();
            if(interactableObject != null)
            {
                //call the interact method via Interface
                interactableObject.Interact(transform);
            }
        }
    }

    public IInteract GetInteractableObject()
    {
        //create a list of interactable objects
        List<IInteract> interactableList = new List<IInteract>();

        //get all colliders in the range of the player
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //check if the collider has the NPCInteract component then add it to the list
            if (collider.TryGetComponent(out IInteract interactable))
            {
                interactableList.Add(interactable);
            }
        }
        IInteract closestInteractable = null;

        //find the closest NPCInteract object from the list
        foreach (IInteract interactable in interactableList)
        {
            if(closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                //check if the distance to the current NPCInteract object is less than the distance to the closest one
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, 
                                                                                            closestInteractable.GetTransform().position))
                {
                    //new closer npc
                    closestInteractable = interactable;
                }
            }
        }
        return closestInteractable;
    }

}
