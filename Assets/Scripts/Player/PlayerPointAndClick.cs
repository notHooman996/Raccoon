using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPointAndClick : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 previousMousePosition;

    private float stoppingDistance = 1.5f; 

    // hit point 
    private Vector3 entryPoint;
    
    private void Start()
    {
        mainCamera = Camera.main;
        
        // initialize the precious mouse position 
        previousMousePosition = Input.mousePosition; 
    }

    private void Update()
    {
        // check if the current mouse position is different from the previous mouse position 
        if (InputHandler.Instance.GetMousePosition() != previousMousePosition)
        {
            previousMousePosition = InputHandler.Instance.GetMousePosition();
            MouseHover();
        }

        // check if the player clicks 
        if (InputHandler.Instance.GetMouseLeftClick())
        {
            MouseClick();
        }

        if (AttributesPointAndClick.Instance.IsPathFindingEnabled)
        {
            FollowPath(PathGenerator.Instance.path);
        }
    }
    
    private void MouseHover()
    {
        // cast a ray from the mouse position in relation to the camera's view 
        Ray ray = mainCamera.ScreenPointToRay(InputHandler.Instance.GetMousePosition());
        RaycastHit hit; 
        
        // check if the ray hits an object 
        if (Physics.Raycast(ray, out hit))
        {
            // check if the object is of type "Interactable"
            if (hit.collider.CompareTag("Interactable"))
            {
                // set hover to be for interactable 
                AttributesPointAndClick.Instance.MouseHover = MouseHoverType.Interactable;
                
                // set the type of interaction possible at the object 
                AttributesPointAndClick.Instance.MouseInteractableHover = hit.collider.GetComponent<InteractableObject>().GetInteractableType();
            }
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                // set hover to be for ground 
                AttributesPointAndClick.Instance.MouseHover = MouseHoverType.Ground;
            }
            else
            {
                // set hover to be none 
                AttributesPointAndClick.Instance.MouseHover = MouseHoverType.None;
            }
        }
        else
        {
            // set hover to be none 
            AttributesPointAndClick.Instance.MouseHover = MouseHoverType.None;
        }
    }

    private void MouseClick()
    {
        // cast a ray from the mouse position in relation to the camera's view 
        Ray ray = mainCamera.ScreenPointToRay(InputHandler.Instance.GetMousePosition());
        RaycastHit hit; 
        
        // check if the ray hits an object 
        if (Physics.Raycast(ray, out hit))
        {
            AttributesPointAndClick.Instance.IsPathFindingEnabled = true; 
            
            // check if the object is of type "Interactable"
            if (hit.collider.CompareTag("Interactable"))
            {
                // set the current objective to interacting 
                AttributesPointAndClick.Instance.CurrentObjective = CurrentObjective.Interact;
                
                // set the chosen interactable object 
                AttributesPointAndClick.Instance.InteractableObject = hit.collider.GameObject();
                
                // testing 
                GameObject testingObject = hit.collider.GameObject();
                InteractableObject testingInteractable = testingObject.GetComponent("InteractableObject") as InteractableObject;
                Debug.Log("Interactible type: " + testingInteractable.GetInteractableType());
                
                // check if player can interact 
                if (AttributesPlayer.Instance.CanPlayerInteract)
                {
                    // if player is colliding with an interactable object, it can interact 
                    Debug.Log("click interact, interact");
                    
                    // TODO - player interacts with object 
                }
                else
                {
                    // move to interactable object position 
                    Debug.Log("click interact, move"); // TODO - move to position 
                    
                    entryPoint = hit.point; 
                    Debug.Log("entrypoint: "+entryPoint);
                    AttributesPointAndClick.Instance.GoalPosition = hit.point;
                }
            }
            // check if the object is of type "StageChanger"
            else if (hit.collider.CompareTag("StageChanger"))
            {
                // set the current objective to change stage 
                AttributesPointAndClick.Instance.CurrentObjective = CurrentObjective.ChangeStage;
                
                // set the chosen object 
                AttributesPointAndClick.Instance.InteractableObject = hit.collider.GameObject();
            }
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                // set the current objective to moving 
                AttributesPointAndClick.Instance.CurrentObjective = CurrentObjective.Move;
                
                Debug.Log("click move"); // TODO - move to position 

                entryPoint = hit.point; 
                Debug.Log("entrypoint: "+entryPoint);
                AttributesPointAndClick.Instance.GoalPosition = entryPoint;
                PathGenerator.Instance.CreateGraph();
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Debug.Log("test gizmos");
        
        Gizmos.color = Color.red; 
        
        Gizmos.DrawSphere(AttributesPointAndClick.Instance.GoalPosition, 0.5f);
        
        Gizmos.DrawRay(AttributesPointAndClick.Instance.GoalPosition, Vector3.up * 10);
    }

    private void FollowPath(List<Vertex> vertices)
    {
        if (vertices?.Count >= 2)
        {
            // TODO - what should be done 
            // turn the player to point towards the next point 
            // if angle is around 20 about the angle it should be, then go straight ahead with x speed 
            // when player is n from end, then stop (decelerate)
            
            // what we no now 
            
            
            
            if (vertices[1].name == "End" && Vector3.Distance(transform.position, vertices[1].position) <= stoppingDistance)
            {
                // when player is x from end, then stop (decelerate) 
                AttributesPointAndClick.Instance.IsPathFindingEnabled = false; 
                Debug.Log("Test");
            }
            else
            {
                // move towards the next vertex 
                transform.LookAt(new Vector3(vertices[1].position.x, transform.position.y, vertices[1].position.z));
                transform.Translate(Vector3.forward * AttributesPlayer.Instance.GetPlayerSpeed() * Time.deltaTime);
            }
        }
    }
}
