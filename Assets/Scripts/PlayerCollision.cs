using UnityEngine;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    private bool isFirstCamera = true; 
    
    private void OnTriggerEnter(Collider other)
    {
        isFirstCamera = !isFirstCamera;
        
        if (isFirstCamera)
        {
            CameraHandler.Instance.SwitchCamera(CameraHandler.Instance.GetCameraByName("Camera1"));
        }
        else
        {
            CameraHandler.Instance.SwitchCamera(CameraHandler.Instance.GetCameraByName("Camera2"));
        }
    }
}
