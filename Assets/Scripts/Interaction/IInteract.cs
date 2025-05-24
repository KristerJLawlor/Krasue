using UnityEngine;

// IInteract interface defines the contract for interactable objects in the game.
// Simplifies the required code for interaction by providing a common method signature for all interactable objects.
public interface IInteract
{
    void Interact(Transform interactorTransform);

    string GetInteractText();

    Transform GetTransform();
}
