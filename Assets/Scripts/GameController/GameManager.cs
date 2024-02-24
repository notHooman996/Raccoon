using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton 
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    private GameData gameData;

    private int sceneIndex;

    private void Start()
    {
        sceneIndex = gameData.sceneIndex; 
    }

    public int GetCurrentSceneIndex()
    {
        return sceneIndex; 
    }

    public void RestartGame()
    {
        sceneIndex = 0;
        gameData.sceneIndex = sceneIndex; 
    }
}