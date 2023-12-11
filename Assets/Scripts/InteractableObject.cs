using UnityEngine;
using Unity.Collections;

public class InteractableObject : MonoBehaviour
{
    [Header("InteractableType")] 
    [SerializeField, ReadOnly] private InteractableType type;

    public InteractableType GetInteractableType()
    {
        return type; 
    }
}
