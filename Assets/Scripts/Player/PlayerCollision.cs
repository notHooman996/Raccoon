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
            Attributes.Instance.SetCanPlayerInteract(true);
            Debug.Log("can interact: "+Attributes.Instance.GetCanPlayerInteract());
        }
        
        if (other.gameObject.CompareTag("SceneChanger"))
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
            Attributes.Instance.SetCanPlayerInteract(false);
            Debug.Log("can interact: "+Attributes.Instance.GetCanPlayerInteract());
        }
    }
}
