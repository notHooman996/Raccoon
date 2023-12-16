using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        KeyboardControllerInteraction();
        PointAndClickInteraction();
    }

    private void KeyboardControllerInteraction()
    {
        if (Attributes.Instance.CanPlayerInteract)
        {
            if (InputHandler.Instance.GetInteractInput())
            {
                Debug.Log("interact - keyboard, controller");
            }
        }
    }

    private void PointAndClickInteraction()
    {
        if (AttributesPointAndClick.Instance.CurrentObjective == CurrentObjective.Interact)
        {
            // check if player is close enough to the chosen interactable object 
            float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, AttributesPointAndClick.Instance.InteractableObject.transform.position);
            if (distance <= AttributesPointAndClick.Instance.InteractDistance)
            {
                Debug.Log("interact - point and click");
            }
        }
    }
}
