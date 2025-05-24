using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Animations.Rigging;

public class NPCInteract : MonoBehaviour, IInteract
{
    [SerializeField] private string interactText;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact(): ");
        InteractText.Create(
            transform.transform, 
            new Vector3(0f, 2.2f, 0f), 
            InteractText.IconType.MouseLkey, 
            "*speaking in thai*");

        //animator.SetTrigger("Talk");

        float playerHeight = 1.7f;
       
        npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
