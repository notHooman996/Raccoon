using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropSelect : EditorWindow
{
    private int selectedBackdropIndex = -1; 
    private int selectedLayerIndex = -1; 
    
    public static GameObject SelectedBackdrop { get; private set; }
    public static GameObject SelectedLayer { get; private set; }
    
    public void DrawSelect()
    {
        GUILayout.BeginVertical("box");
        Deselect();
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
            SelectedBackdrop = null;
            SelectedLayer = null;

            selectedBackdropIndex = -1;
            selectedLayerIndex = -1; 
        }
        
        // reset color to default 
        GUI.backgroundColor = BackdropToolUtilities.DefaultColor; 
    }

    private void SelectBackdrop()
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