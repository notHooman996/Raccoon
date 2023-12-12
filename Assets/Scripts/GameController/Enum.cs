using UnityEngine;

/// <summary>
/// Type of object mouse is hovering over, used for setting the cursor icon 
/// </summary>
public enum MouseHoverType {Ground, Interactable, None}

/// <summary>
/// Type of the interactable object, used for setting the cursor icon 
/// </summary>
public enum InteractableType {Test, Hide}

/// <summary>
/// Type of point and click objective, used to determine the current point and click objective 
/// </summary>
public enum CurrentObjective {Move, Interact}

public class Enum : MonoBehaviour
{
    // class should be here, but no code is needed 
    // only place for collecting enum 
}