using Cinemachine;
using UnityEngine;

public class PlayerPointAndClick : MonoBehaviour
{
    private Camera mainCamera; 
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MouseHover();
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
                // if it is an "Interactable" object, perform the desired actions 
                // TODO - the mouse gets a hover icon relating to the interaction that can be performed 
                
                Debug.Log("interactable");
            }
            // check if the object is of type "Ground" 
            else if (hit.collider.CompareTag("Ground"))
            {
                // if it is an "Ground" object, perform the desired actions 
                // TODO - the player is able to click the ground 
                
                Debug.Log("ground");
            }
        }
        
    }
}
