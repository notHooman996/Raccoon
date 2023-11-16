using UnityEngine;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject cameraController; 
    
    private bool isFirstCamera = true; 
    
    private void OnTriggerEnter(Collider other)
    {
        isFirstCamera = !isFirstCamera;
        
        if (isFirstCamera)
        {
            cameraController.GetComponent<CameraHandler>().SwitchCamera(cameraController.GetComponent<CameraHandler>().cameras[0]);
        }
        else
        {
            cameraController.GetComponent<CameraHandler>().SwitchCamera(cameraController.GetComponent<CameraHandler>().cameras[1]);
        }
    }
}
