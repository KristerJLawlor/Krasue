using UnityEngine;
using TMPro;

public class InteractPopup : MonoBehaviour
{
    //Enable or disable the popup when the player is in range

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;


    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }
    private void Show(NPCInteract npcInteract)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteract.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }

}
