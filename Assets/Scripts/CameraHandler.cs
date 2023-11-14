using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera camera1; 
    public CinemachineVirtualCamera camera2;

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCamera; 
    
    private void Start()
    {
        InitialSetup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchCamera(camera2);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SwitchCamera(camera1);
        }
    }

    public void InitialSetup()
    {
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

    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
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
}
