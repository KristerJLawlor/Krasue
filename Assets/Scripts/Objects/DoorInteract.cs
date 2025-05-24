using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteract
{
    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen); 
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleDoor();
    }

    public string GetInteractText()
    {
        return "Open Door";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
