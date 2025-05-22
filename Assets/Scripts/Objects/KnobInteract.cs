using UnityEngine;

public class KnobInteract : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material yellowMaterial;

    private bool isColorYellow;

    private void SetColorBlue()
    {
        meshRenderer.material = blueMaterial;
    }

    private void SetColorYellow()
    {
        meshRenderer.material = yellowMaterial;
    }

    private void ToggleColor()
    {
        isColorYellow = !isColorYellow;
        if (isColorYellow)
        {
            SetColorYellow();
        }
        else
        {
            SetColorBlue();
        }
    }

    public void PushButton()
    {
        ToggleColor();
    }
}
