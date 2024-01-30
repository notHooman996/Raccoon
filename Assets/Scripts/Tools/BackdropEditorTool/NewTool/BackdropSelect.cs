using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropSelect : EditorWindow
{
    public GameObject SelectedBackdrop { get; private set; }
    public GameObject SelectedLayer { get; private set; }
    
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
        if (GUILayout.Button("Deselect Backdrop"))
        {
            // TODO 
            // make sure the selected objects can be deselected 
        }
    }

    private void SelectBackdrop()
    {
        // TODO 
        // make an object field where the user can select a backdrop 
        // make sure the only objects available to be selected are backdrop objects in the scene! 
        SelectedBackdrop = EditorGUILayout.ObjectField("Select Backdrop: ", SelectedBackdrop, typeof(GameObject), true) as GameObject;
    }

    private void SelectLayer()
    {
        // TODO 
        // make sure backdrop is selected first 
        // make an object field where the user can select a layer 
        // make sure the only objects available to be selected are layers on the selected backdrop! 
        SelectedLayer = EditorGUILayout.ObjectField("Select Layer: ", SelectedLayer, typeof(GameObject), true) as GameObject;
    }
}