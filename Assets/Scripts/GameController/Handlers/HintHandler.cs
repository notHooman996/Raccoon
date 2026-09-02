using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HintHandler : MonoBehaviour
{
    private bool isHintActive = false; 
    private float despawnTime = 3; 
    
    // reference to the prefab 
    private GameObject hintPrefab;
    
    public List<GameObject> interactables = new List<GameObject>();
    
    private float maxDistance = 2f;
    
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
        foreach (GameObject interactable in interactables)
        {
            Vector3 hintPosition;
            
            RaycastHit hit;
            // do raycast to determine the height of a hint 
            if (Physics.Raycast(interactable.transform.position, -Vector3.up, out hit, maxDistance, LayerMask.GetMask("Ground")))
            {
                // if point is not too high 
                hintPosition = interactable.transform.position; 
            }
            else
            {
                // if the point is too high up, set at max height 
                hintPosition = new Vector3(interactable.transform.position.x, maxDistance, interactable.transform.position.z);
            }
            
            GameObject hintGameObject = Instantiate(hintPrefab, hintPosition, Quaternion.identity);
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
