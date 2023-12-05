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
    
    [Header("CanPlayerInteract")]
    [SerializeField, ReadOnly] private bool isMouseHovering;
    
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

    public void SetActiveVirtualCamera(CinemachineVirtualCameraBase virtualCamera)
    {
        activeVirtualCamera = virtualCamera;
    }

    public CinemachineVirtualCameraBase GetActiveVirtualCamera()
    {
        return activeVirtualCamera;
    }
    
    public void SetCanPlayerMove()
    {
        canPlayerMove = !canPlayerMove;
    }
    
    public bool GetCanPlayerMove()
    {
        return canPlayerMove; 
    }
    
    public void SetCanPlayerInteract(bool b)
    {
        canPlayerInteract = b;
    }
    
    public bool GetCanPlayerInteract()
    {
        return canPlayerInteract; 
    }

    private void SetMouseHover(bool b)
    {
        isMouseHovering = b; 
    }
}
