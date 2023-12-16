using Cinemachine;
using UnityEngine;
using Unity.Collections;

public class Attributes : MonoBehaviour
{
    // singleton 
    public static Attributes Instance;
    
    [Header("ActiveVirtualCamera")] 
    [SerializeField, ReadOnly] private CinemachineVirtualCameraBase activeVirtualCamera;

    [Header("CanPlayerMove")] 
    [SerializeField, ReadOnly] private bool canPlayerMove;
    
    [Header("CanPlayerInteract")]
    [SerializeField, ReadOnly] private bool canPlayerInteract = false;
    
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
}
