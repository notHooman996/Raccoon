using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEditor;

public class MouseIconHandler : MonoBehaviour
{
    private Dictionary<string, Texture2D> icons = new Dictionary<string, Texture2D>();
    
    private bool isPreviousPause;
    private MouseHoverType previousMouseHoverType;
    private InteractableType previousInteractableType;

    private void Start()
    {
        // load textures // TODO - change to correct textures later 
        icons.Add("cursorTestTexture", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MouseIcons/TestIcon.png"));
        icons.Add("leafTexture", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MouseIcons/TestIcon.png"));
        icons.Add("walkTexture", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MouseIcons/TestIcon.png"));
        icons.Add("hideTexture", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MouseIcons/TestIcon.png"));
        
        isPreviousPause = InputHandler.Instance.GetPauseInput();
        previousMouseHoverType = AttributesPointAndClick.Instance.MouseHover;
        previousInteractableType = AttributesPointAndClick.Instance.MouseInteractableHover;

        // set default cursor icon 
        SetCursorIcon(icons["leafTexture"]);
    }

    private void Update()
    {
        if (InputHandler.Instance.GetIsMouseInput())
        {
            Cursor.visible = true; 
            
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
        else
        {
            Cursor.visible = false; 
        }
    }

    private void UpdateCursorIcon()
    {
        // check if game is paused, hence in menu state 
        if (InputHandler.Instance.GetPauseInput())
        {
            SetCursorIcon(icons["leafTexture"]);
        }
        else if(AttributesPointAndClick.Instance.MouseHover == MouseHoverType.Interactable)
        {
            // set depending on the interactable type 
            SetInteractableCursor();
        }
        else if(AttributesPointAndClick.Instance.MouseHover == MouseHoverType.Ground)
        {
            // set depending on ground 
            SetCursorIcon(icons["walkTexture"]);
        }
        else
        {
            // set cursor to normal 
            SetCursorIcon(icons["leafTexture"]);
        }
    }

    private void SetInteractableCursor()
    {
        switch (AttributesPointAndClick.Instance.MouseInteractableHover)
        {
            case InteractableType.Test:
                SetCursorIcon(icons["cursorTestTexture"]);
                break; 
            case InteractableType.Hide:
                SetCursorIcon(icons["hideTexture"]);
                break; 
        }
    }

    private void SetCursorIcon(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);
    }
}