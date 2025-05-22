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
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteract npcInteract))
                {
                    npcInteract.Interact(transform);
                }
                if (collider.TryGetComponent(out KnobInteract knobInteract))
                {
                    knobInteract.PushButton();
                    npcInteract.Interact(transform);
                }
            }
        }

    }

    public NPCInteract GetInteractableObject()
    {
        //create a list of interactable objects
        List<NPCInteract> interactableNPCList = new List<NPCInteract>();

        //get all colliders in the range of the player
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //check if the collider has the NPCInteract component then add it to the list
            if (collider.TryGetComponent(out NPCInteract npcInteract))
            {
                interactableNPCList.Add(npcInteract);
            }
        }
        NPCInteract closestNPC = null;

        //find the closest NPCInteract object from the list
        foreach (NPCInteract npcInteract in interactableNPCList)
        {
            if(closestNPC == null)
            {
                closestNPC = npcInteract;
            }
            else
            {
                //check if the distance to the current NPCInteract object is less than the distance to the closest one
                if (Vector3.Distance(transform.position, npcInteract.transform.position) < Vector3.Distance(transform.position, closestNPC.transform.position))
                {
                    //new closer npc
                    closestNPC = npcInteract;
                }
            }
        }
        return closestNPC;
    }

}
