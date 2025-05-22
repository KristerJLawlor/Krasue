using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TogggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen); 
    }
}
