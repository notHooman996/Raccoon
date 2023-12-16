using UnityEngine;
using Unity.Collections;

public class MouseIconHandler : MonoBehaviour
{
    [Header("CursorTextures")] 
    [SerializeField, ReadOnly] private Texture2D cursorTestTexture; 
    [SerializeField, ReadOnly] private Texture2D leafTexture; 
    [SerializeField, ReadOnly] private Texture2D walkTexture; 
    [SerializeField, ReadOnly] private Texture2D hideTexture;

    private bool isPreviousPause;
    private MouseHoverType previousMouseHoverType;
    private InteractableType previousInteractableType; 
    
    private void Start()
    {
        isPreviousPause = InputHandler.Instance.GetPauseInput();
        previousMouseHoverType = AttributesPointAndClick.Instance.MouseHover;
        previousInteractableType = AttributesPointAndClick.Instance.MouseInteractableHover;

        // set default cursor icon 
        SetCursorIcon(leafTexture);
    }

    private void Update()
    {
        if (isPreviousPause != InputHandler.Instance.GetPauseInput() ||
            previousMouseHoverType != AttributesPointAndClick.Instance.MouseHover ||
            previousInteractableType != AttributesPointAndClick.Instance.MouseInteractableHover)
        {
            // only update cursor icon when it needs to change 
            UpdateCursorIcon();
            
            // update previous variables 
            isPreviousPause = InputHandler.Instance.GetPauseInput();
            previousMouseHoverType = AttributesPointAndClick.Instance.MouseHover;
            previousInteractableType = AttributesPointAndClick.Instance.MouseInteractableHover;
        }
    }

    private void UpdateCursorIcon()
    {
        // check if game is paused, hence in menu state 
        if (InputHandler.Instance.GetPauseInput())
        {
            SetCursorIcon(leafTexture);
        }
        else if(AttributesPointAndClick.Instance.MouseHover == MouseHoverType.Interactable)
        {
            // set depending on the interactable type 
            SetInteractableCursor();
        }
        else if(AttributesPointAndClick.Instance.MouseHover == MouseHoverType.Ground)
        {
            // set depending on ground 
            SetCursorIcon(walkTexture);
        }
        else
        {
            // set cursor to normal 
            SetCursorIcon(leafTexture);
        }
    }

    private void SetInteractableCursor()
    {
        switch (AttributesPointAndClick.Instance.MouseInteractableHover)
        {
            case InteractableType.Test:
                SetCursorIcon(cursorTestTexture);
                break; 
            case InteractableType.Hide:
                SetCursorIcon(hideTexture);
                break; 
        }
    }

    private void SetCursorIcon(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
