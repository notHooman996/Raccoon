using UnityEngine;
using Unity.Collections;

public class AttributesPointAndClick : MonoBehaviour
{
    // singleton 
    public static AttributesPointAndClick Instance;
    
    [Header("MouseHovering")]
    [SerializeField, ReadOnly] private MouseHoverType mouseHovering;
    [SerializeField, ReadOnly] private InteractableType mouseInteractableHover;
    
    [Header("Objective")] 
    [SerializeField, ReadOnly] private CurrentObjective currentObjective;
    [SerializeField, ReadOnly] private Vector3 goalPosition;
    [SerializeField, ReadOnly] private GameObject interactableObject;
    [SerializeField, ReadOnly] private float interactDistance = 3;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    public void SetMouseHover(MouseHoverType type)
    {
        mouseHovering = type; 
    }

    public MouseHoverType GetMouseHover()
    {
        return mouseHovering; 
    }

    public void SetMouseInteractableHover(InteractableType type)
    {
        mouseInteractableHover = type; 
    }

    public InteractableType GetMouseInteractableHover()
    {
        return mouseInteractableHover; 
    }

    public void SetCurrentObjective(CurrentObjective objective)
    {
        currentObjective = objective; 
    }

    public CurrentObjective GetCurrentObjective()
    {
        return currentObjective; 
    }

    public void SetGoalPosition(Vector3 position)
    {
        goalPosition = position;
    }

    public Vector3 GetGoalPosition()
    {
        return goalPosition; 
    }

    public void SetInteractable(GameObject obj)
    {
        interactableObject = obj; 
    }

    public GameObject GetInteractable()
    {
        return interactableObject; 
    }

    public float GetInteractDistance()
    {
        return interactDistance; 
    }
}
