using System;
using UnityEngine;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    private bool isFirstCamera = true; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            AttributesPlayer.Instance.CanPlayerInteract = true;
            Debug.Log("can interact: "+AttributesPlayer.Instance.CanPlayerInteract);
        }
        
        if (other.gameObject.CompareTag("SceneChanger")) // TODO - camera - handle on object, not player 
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            AttributesPlayer.Instance.CanPlayerInteract = false;
            Debug.Log("can interact: "+AttributesPlayer.Instance.CanPlayerInteract);
        }
    }
}
