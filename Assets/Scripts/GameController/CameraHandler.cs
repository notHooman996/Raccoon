using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    // singleton 
    public static CameraHandler Instance; 
    
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCamera; 
    private CinemachineVirtualCamera prevCamera;

    public CinemachineVirtualCamera GetCurrentCamera
    {
        get { return currentCamera; }
    }
    
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
        InitialSetup();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Instantiates the cameras. Sets the initial priorities for all cameras. 
    /// </summary>
    public void InitialSetup()
    {
        prevCamera = startCamera; 
        currentCamera = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCamera)
            {
                cameras[i].Priority = 20; 
            }
            else
            {
                cameras[i].Priority = 10; 
            }
        }
    }

    /// <summary>
    /// Finds the camera in the list of cameras in the scene by the given name. 
    /// </summary>
    /// <param name="cameraName">Name of camera</param>
    /// <returns>Camera with the given name</returns>
    public CinemachineVirtualCamera GetCameraByName(string cameraName)
    {
        foreach (CinemachineVirtualCamera virtualCamera in cameras)
        {
            if (virtualCamera.Name == cameraName)
            {
                return virtualCamera;
            }
        }

        return null; 
    }
    
    /// <summary>
    /// Switches cameras when promted to. 
    /// </summary>
    /// <param name="newCamera">The camera that should be changed to</param>
    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        prevCamera = currentCamera; 
        currentCamera = newCamera;

        currentCamera.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCamera)
            {
                cameras[i].Priority = 10; 
            }
        }
    }

    /// <summary>
    /// Switches back to the previous camera. 
    /// </summary>
    public void SwitchToPrevCamera()
    {
        SwitchCamera(prevCamera);
    }
}
