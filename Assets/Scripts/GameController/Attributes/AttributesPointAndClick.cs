using UnityEngine;
using Unity.Collections;

/// <summary>
/// Type of object mouse is hovering over, used for setting the cursor icon 
/// </summary>
public enum MouseHoverType {Ground, Interactable, None}

/// <summary>
/// Type of the interactable object, used for setting the cursor icon 
/// </summary>
public enum InteractableType {Test, Hide, StageChange}

/// <summary>
/// Type of point and click objective, used to determine the current point and click objective 
/// </summary>
public enum CurrentObjective {Move, Interact, ChangeStage}

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
    
    [Header("PathFinding")]
    [SerializeField, ReadOnly] private bool isPathFindingEnabled = false; 
    
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

    public MouseHoverType MouseHover
    {
        get { return mouseHovering; }
        set { mouseHovering = value; }
    }

    public InteractableType MouseInteractableHover
    {
        get { return mouseInteractableHover; }
        set { mouseInteractableHover = value; }
    }

    public CurrentObjective CurrentObjective
    {
        get { return currentObjective; }
        set { currentObjective = value; }
    }

    public Vector3 GoalPosition
    {
        get { return goalPosition; }
        set { goalPosition = value; }
    }

    public GameObject InteractableObject
    {
        get { return interactableObject; }
        set { interactableObject = value; }
    }

    public bool IsPathFindingEnabled
    {
        get { return isPathFindingEnabled; }
        set { isPathFindingEnabled = value; }
    }
}