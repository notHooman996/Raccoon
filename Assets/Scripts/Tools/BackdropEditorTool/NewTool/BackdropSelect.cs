using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropSelect : EditorWindow
{
    private int selectedBackdropHolderIndex = -1; 
    private int selectedBackdropIndex = -1; 
    private int selectedLayerIndex = -1; 
    
    public static GameObject SelectedBackdropHolder { get; private set; }
    public static GameObject SelectedBackdrop { get; private set; }
    public static GameObject SelectedLayer { get; private set; }
    
    public void DrawSelect()
    {
        GUILayout.BeginVertical("box");
        Deselect();
        SelectBackdropHolder();
        SelectBackdrop();
        SelectLayer();
        GUILayout.EndVertical();
    }

    private void Deselect()
    {
        bool isClickable = SelectedBackdrop != null;
        
        Color buttonColor = isClickable ? BackdropToolUtilities.DefaultColor : BackdropToolUtilities.UnavailableColor;
        GUI.backgroundColor = buttonColor; 
        
        if (GUILayout.Button("Deselect Backdrop"))
        {
            SelectedBackdropHolder = null; 
            SelectedBackdrop = null;
            SelectedLayer = null;

            selectedBackdropHolderIndex = -1; 
            selectedBackdropIndex = -1;
            selectedLayerIndex = -1; 
        }
        
        // reset color to default 
        GUI.backgroundColor = BackdropToolUtilities.DefaultColor; 
    }

    private void SelectBackdropHolder()
    {
        if (BackdropLoad.BackdropHolders.Count > 0)
        {
            // display a dropdown with a list of holders 
            string[] holderNames = BackdropLoad.BackdropHolders.ConvertAll(h => h.name).ToArray();
            selectedBackdropHolderIndex = EditorGUILayout.Popup("Selected BackdropHolder: ", selectedBackdropHolderIndex, holderNames);
            
            // set the holder object when something is selected in the popup 
            if (selectedBackdropHolderIndex >= 0 && selectedBackdropHolderIndex < BackdropLoad.BackdropHolders.Count)
            {
                SelectedBackdropHolder = BackdropLoad.BackdropHolders[selectedBackdropHolderIndex];
            }
        }
        else
        {
            SelectedBackdropHolder = null; 
            SelectedBackdrop = null;
            SelectedLayer = null;

            selectedBackdropHolderIndex = -1; 
            selectedBackdropIndex = -1;
            selectedLayerIndex = -1; 
        }
    }

    private void SelectBackdrop()
    {
        if (SelectedBackdropHolder != null && BackdropLoad.Backdrops != null)
        {
            if (BackdropLoad.Backdrops.Count > 0)
            {
                // display a dropdown with a list of backdrops  
                string[] backdropNames = BackdropLoad.Backdrops.ConvertAll(b => b.name).ToArray();
                selectedBackdropIndex = EditorGUILayout.Popup("Selected Backdrop: ", selectedBackdropIndex, backdropNames);

                // set the backdrop gameobject when something is selected in the popup 
                if (selectedBackdropIndex >= 0 && selectedBackdropIndex < BackdropLoad.Backdrops.Count)
                {
                    SelectedBackdrop = BackdropLoad.Backdrops[selectedBackdropIndex];
                }
            }
            else
            {
                SelectedBackdrop = null;
                SelectedLayer = null;

                selectedBackdropIndex = -1;
                selectedLayerIndex = -1;
            }
        }
    }

    private void SelectLayer()
    {
        if (SelectedBackdrop != null && BackdropLoad.Layers != null)
        {
            if (BackdropLoad.Layers.Count > 0)
            {
                // display a dropdown with a list of backdrops  
                string[] layerNames = BackdropLoad.Layers.ConvertAll(b => b.name).ToArray();
                selectedLayerIndex = EditorGUILayout.Popup("Selected Layer: ", selectedLayerIndex, layerNames);

                // set the backdrop gameobject when something is selected in the popup 
                if (selectedLayerIndex >= 0 && selectedLayerIndex < BackdropLoad.Layers.Count)
                {
                    SelectedLayer = BackdropLoad.Layers[selectedLayerIndex];
                }
            }
            else
            {
                SelectedLayer = null;
                selectedLayerIndex = -1; 
            }
        }
    }
}