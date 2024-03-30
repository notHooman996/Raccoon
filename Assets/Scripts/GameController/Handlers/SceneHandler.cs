using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // singleton 
    public static SceneHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    [Header("List of Scenes")]
    [SerializeField, ReadOnly] private List<Scene> scenes = new List<Scene>();
    
    private int currentSceneIndex = 0; // TODO - get the index from GameManager 
    
    public Scene CurrentScene
    {
        get { return scenes[currentSceneIndex]; }
    }
}
