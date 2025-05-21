using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCInteract : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        InteractText.Create(
            transform.transform, 
            new Vector3(-0.3f, 1.5f, 0f), 
            InteractText.IconType.MouseLkey, 
            "*speaking in thai*");

        animator.SetTrigger("Talk");
    }


}
