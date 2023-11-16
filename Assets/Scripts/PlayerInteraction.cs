using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (InputHandler.Instance.GetInteractInput())
        {
            Debug.Log("interact");
        }
    }
}
