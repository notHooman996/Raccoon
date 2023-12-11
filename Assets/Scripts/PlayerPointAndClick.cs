using Cinemachine;
using UnityEngine;

public class PlayerPointAndClick : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 previousMousePosition; 
    
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
                Attributes.Instance.SetMouseHover(global::MouseHoverType.Interactable);
                
                // set the type of interaction possible at the object 
                Attributes.Instance.SetMouseInteractableHover(hit.collider.GetComponent<InteractableObject>().GetInteractableType());
            }
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                // set hover to be for ground 
                Attributes.Instance.SetMouseHover(global::MouseHoverType.Ground);
            }
            else
            {
                // set hover to be none 
                Attributes.Instance.SetMouseHover(global::MouseHoverType.None);
            }
        }
        else
        {
            // set hover to be none 
            Attributes.Instance.SetMouseHover(global::MouseHoverType.None);
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
                // check if player can interact 
                if (Attributes.Instance.GetCanPlayerInteract())
                {
                    // if player is colliding with an interactable object, it can interact 
                    Debug.Log("click interact, interact");
                    
                    // TODO - player interacts with object 
                }
                else
                {
                    // move to interactable object position 
                    Debug.Log("click interact, move"); // TODO - move to position 
                }
            }
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("click move"); // TODO - move to position 
            }
        }
    }
}
