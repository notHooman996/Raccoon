using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isFirstCamera = true; 
    
    private void OnTriggerEnter(Collider other)
    {
        isFirstCamera = !isFirstCamera;

        if (isFirstCamera)
        {
            gameObject.GetComponent<CameraHandler>().SwitchCamera(gameObject.GetComponent<CameraHandler>().camera1);
        }
        else
        {
            gameObject.GetComponent<CameraHandler>().SwitchCamera(gameObject.GetComponent<CameraHandler>().camera2);
        }
    }
}
