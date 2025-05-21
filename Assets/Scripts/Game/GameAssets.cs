using UnityEngine;

//This script acts as a hub to load any game asset prefab that we need in the game via script access
public class GameAssets : MonoBehaviour
{

    private static GameAssets _instance;

    public static GameAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _instance;
        }   
    }

    public Sprite uiBackground;

    public Transform interactText;

}
