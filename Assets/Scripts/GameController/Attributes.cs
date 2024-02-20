using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Unity.Collections;
using UnityEngine.SceneManagement;

public class Attributes : MonoBehaviour
{
    // singleton 
    public static Attributes Instance;
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
    
    [Header("ActiveVirtualCamera")] 
    [SerializeField, ReadOnly] private CinemachineVirtualCameraBase activeVirtualCamera;

    [Header("CanPlayerMove")] 
    [SerializeField, ReadOnly] private bool canPlayerMove;
    
    [Header("CanPlayerInteract")]
    [SerializeField, ReadOnly] private bool canPlayerInteract = false; // TODO - move somewhere else 
    
    [Header("List of Scenes")]
    [SerializeField, ReadOnly] private List<Scene> scenes = new List<Scene>();

    private int currentSceneIndex = 0; 
    
    private void Start()
    {
        canPlayerMove = true; // TODO - set to false when game starts 
    }

    private void Update()
    {
        
    }

    public CinemachineVirtualCameraBase ActiveVirtualCamera
    {
        get { return activeVirtualCamera; }
        set { activeVirtualCamera = value; }
    }

    public bool CanPlayerMove
    {
        get { return canPlayerMove; }
        set { canPlayerMove = value; }
    }

    public bool CanPlayerInteract
    {
        get { return canPlayerInteract; }
        set { canPlayerInteract = value; }
    }

    public Scene CurrentScene
    {
        get { return scenes[currentSceneIndex]; }
    }
}
