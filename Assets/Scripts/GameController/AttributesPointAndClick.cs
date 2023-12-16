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

    public float InteractDistance
    {
        get { return interactDistance; }
    }
}
