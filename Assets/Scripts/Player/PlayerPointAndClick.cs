using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPointAndClick : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 previousMousePosition;

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
            // check if the object is of type "Interactable"
            if (hit.collider.CompareTag("Interactable"))
            {
                // set the current objective to interacting 
                AttributesPointAndClick.Instance.CurrentObjective = CurrentObjective.Interact;
                
                // set the chosen interactable object 
                AttributesPointAndClick.Instance.InteractableObject = hit.collider.GameObject();
                
                // check if player can interact 
                if (Attributes.Instance.CanPlayerInteract)
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
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                // set the current objective to moving 
                AttributesPointAndClick.Instance.CurrentObjective = CurrentObjective.Move;
                
                Debug.Log("click move"); // TODO - move to position 

                entryPoint = hit.point; 
                Debug.Log("entrypoint: "+entryPoint);
                AttributesPointAndClick.Instance.GoalPosition = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.Log("test gizmos");
        
        Gizmos.color = Color.red; 
        
        Gizmos.DrawSphere(entryPoint, 0.5f);
        
        Gizmos.DrawRay(entryPoint, Vector3.up * 10);
    }
}
