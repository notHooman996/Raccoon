using System;
using UnityEngine;
using Cinemachine;

public class StageChanger : MonoBehaviour
{
    [SerializeField] private GameObject stage1; 
    [SerializeField] private GameObject stage2;

    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;

    [SerializeField] private Vector3 position1; 
    [SerializeField] private Vector3 position2; 

    public GameObject GetStage1()
    {
        return stage1;
    }
    public GameObject GetStage2()
    {
        return stage2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enter");
            // change the stage when hitting the stage changer 
            if (StageHandler.Instance.CurrentStage == stage1)
            {
                StageHandler.Instance.SetCurrentStage(stage2);
                PlayerForcedMovement.ForcedPosition = position2;
                PlayerForcedMovement.ShouldMoveForced = true; 
            }
            else if (StageHandler.Instance.CurrentStage == stage2)
            {
                StageHandler.Instance.SetCurrentStage(stage1);
                PlayerForcedMovement.ForcedPosition = position1;
                PlayerForcedMovement.ShouldMoveForced = true; 
            }
            else
            {
                Debug.Log("Error: startStage, currentStage, stage1 or stage2 is not set.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exit");
            // change the camera when leaving the stage changer 
            if (StageHandler.Instance.CurrentStage == stage1)
            {
                CameraHandler.Instance.SwitchCamera(camera1);
            }
            else if (StageHandler.Instance.CurrentStage == stage2)
            {
                CameraHandler.Instance.SwitchCamera(camera2);
            }
            else
            {
                Debug.Log("Error: camera1 or camera2 is not set.");
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(position1, 0.3f);
        Gizmos.DrawSphere(position2, 0.3f);
    }
}
