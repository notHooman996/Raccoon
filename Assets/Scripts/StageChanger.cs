using System;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    [SerializeField] private GameObject stage1; 
    [SerializeField] private GameObject stage2;

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
            Debug.Log("player");
            if (StageHandler.Instance.CurrentStage == stage1)
            {
                Debug.Log("stage 2");
                StageHandler.Instance.SetCurrentStage(stage2);
            }
            else if (StageHandler.Instance.CurrentStage == stage2)
            {
                Debug.Log("stage 1");
                StageHandler.Instance.SetCurrentStage(stage1);
            }
            else
            {
                Debug.Log("Error: startStage, currentStage, stage1 or stage2 is not set.");
            }
        }
    }
}
