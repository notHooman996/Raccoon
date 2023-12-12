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
        if (Attributes.Instance.GetCanPlayerInteract())
        {
            if (InputHandler.Instance.GetInteractInput())
            {
                Debug.Log("interact - keyboard, controller");
            }
        }
    }

    private void PointAndClickInteraction()
    {
        if (Attributes.Instance.GetCurrentObjective() == CurrentObjective.Interact)
        {
            // check if player is close enough to the chosen interactable object 
            float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, Attributes.Instance.GetInteractable().transform.position);
            if (distance <= Attributes.Instance.GetInteractDistance())
            {
                Debug.Log("interact - point and click");
            }
        }
    }
}
