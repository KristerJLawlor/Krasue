using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using System.IO;

public class InteractText : MonoBehaviour
{

    public enum IconType
    {
        Ekey,
        MouseLkey,
        Esckey
    }

    [SerializeField] private Sprite eSprite;
    [SerializeField] private Sprite mouseLSprite;
    [SerializeField] private Sprite escSprite;

    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
    {
        Transform interactTextTransform = Instantiate(GameAssets.instance.interactText, parent);
        interactTextTransform.localPosition = localPosition;
        interactTextTransform.Rotate(0f, 180f, 0f); // Rotate the text to face the camera

        interactTextTransform.GetComponent<InteractText>().Setup(iconType, text);

        //Destroy(interactTextTransform.gameObject, 2f);    // Destroy after 2 seconds if desired
    }

    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(1f, 0.5f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-1.2f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector3
            (backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.sprite = GetIconSprite(iconType);

    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Ekey: return eSprite;
                break;
            case IconType.MouseLkey: return mouseLSprite;
                break;
            case IconType.Esckey: return escSprite;
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    void Start()
    {
        //Setup(IconType.Esckey, "Hi there! Akko is a disgusting piggy!!!!!!!!!!!!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
