using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HintHandler : MonoBehaviour
{
    private Camera mainCamera; 
    
    private bool isHintActive = false; 
    private float despawnTime = 3; 
    
    // reference to the prefab 
    private GameObject hintPrefab;

    private GameObject hintCanvas; 
    
    public List<GameObject> interactables = new List<GameObject>();
    
    private void Start()
    {
        hintPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Hint/Hint.prefab");
    }

    private void Update()
    {
        if (!isHintActive && InputHandler.Instance.GetIsHintInput())
        {
            isHintActive = true; 
            
            ObtainReferenceToInteractables();
            ObtainReferenceToStageChangers();

            SpawnHints();

            StartCoroutine(DespawnHints());
        }
    }

    private void ObtainReferenceToInteractables()
    {
        // get the current scene 
        GameObject currentStage = GameObject.Find("RaccoonHome"); // TODO - change to current stage 
        if (currentStage != null)
        {
            // get all children of the parent object 
            Transform[] children = currentStage.GetComponentsInChildren<Transform>();
            
            // iterate through each child 
            foreach (Transform child in children)
            {
                // check if the child has specific tag 
                if (child.CompareTag("Interactable"))
                {
                    // add to list 
                    interactables.Add(child.gameObject);
                }
            }
        }
    }

    private void ObtainReferenceToStageChangers()
    {
        GameObject currentStage = GameObject.Find("RaccoonHome"); // TODO - change to current stage 
        
        GameObject[] stageChangers = GameObject.FindGameObjectsWithTag("StageChanger");
        foreach (GameObject stageChanger in stageChangers)
        {
            StageChanger stageChangerComponent = stageChanger.GetComponent<StageChanger>();
            if (stageChangerComponent.GetStage1() == currentStage || stageChangerComponent.GetStage2() == currentStage)
            {
                // add to list 
                interactables.Add(stageChanger);
            }
        }
    }

    private void SpawnHints()
    {
        Debug.Log("Spawn");
        foreach (GameObject interactable in interactables)
        {
            GameObject hintGameObject = Instantiate(hintPrefab, interactable.transform.position, Quaternion.identity);

            //GameObject hintGameObject = Instantiate(hintPrefab, interactable.transform.position, Quaternion.identity);
            // GameObject hintGameObject = Instantiate(hintPrefab, hintCanvas.transform);
            // hintGameObject.transform.localPosition = interactable.transform.position;
            // hintGameObject.transform.localRotation = Quaternion.identity; 
        }
    }

    private IEnumerator DespawnHints()
    {
        yield return new WaitForSeconds(despawnTime);
        
        // find all hint gameobjects 
        GameObject[] hints = GameObject.FindGameObjectsWithTag("Hint");
        
        // destroy each hint object 
        foreach (GameObject hint in hints)
        {
            Destroy(hint);
        }
        
        interactables.Clear();

        isHintActive = false; 
    }
}
