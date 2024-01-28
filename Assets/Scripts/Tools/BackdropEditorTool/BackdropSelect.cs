using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropSelect : EditorWindow
{
    public static List<GameObject> Backdrops { get; set; }
    private List<GameObject> layers = new List<GameObject>();
    private bool showBackdropList = false;
    private bool showLayerList = false;

    private int selectedBackdropIndex = -1;
    private int selectedLayerIndex = -1;
    private Color selectedElementColor = new Color(0f, 1f, 0f, 0.15f);
    
    public static GameObject SelectedBackdrop { get; private set; }
    public static GameObject SelectedLayer { get; private set; }
    public static GameObject SelectedFloor { get; private set; }

    public void DrawSelectTab()
    {
        BackdropTool.DrawHorizontalLine();
        Deselect();
        BackdropTool.DrawHorizontalLine();
        
        DrawList(ref showBackdropList,
                "Backdrop GameObject List",
                Backdrops,
                ref selectedBackdropIndex,
                (x) => BackdropElement(x),
                (x) => BackdropEditButton(x));
        
        BackdropTool.DrawHorizontalLine();
        
        DrawList(ref showLayerList,
                "Layer GameObject List",
                layers,
                ref selectedLayerIndex,
                (x) => LayerElement(x),
                (x) => LayerEditButton(x)); 
        
        BackdropTool.DrawHorizontalLine();
    }

    private void Deselect()
    {
        // set button background color 
        Color buttonColor = SelectedBackdrop == null ? Color.grey : Color.white;
        GUI.backgroundColor = buttonColor;
        
        if (GUILayout.Button("Deselect backdrop", GUILayout.MaxWidth(150)))
        {
            SelectedBackdrop = null;
            SelectedLayer = null;
            SelectedFloor = null; 
            
            layers.Clear();

            selectedBackdropIndex = -1;
            selectedLayerIndex = -1; 
        }

        // reset background color for other elements 
        GUI.backgroundColor = Color.white;
    }

    private void DrawList(ref bool showList, string text, List<GameObject> list, ref int selectedIndex, Action<int> elementMethod, Action<int> editMethod)
    {
        showList = EditorGUILayout.Foldout(showList, text);
        if (showList)
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    // draw colored rectangle behind the selected element 
                    if (i == selectedIndex)
                    {
                        Rect rect = EditorGUILayout.BeginHorizontal();
                        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, rect.height), selectedElementColor);
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                    }
                    
                    // call element method 
                    elementMethod(i);

                    if (GUILayout.Button("Edit", GUILayout.MaxWidth(70)))
                    {
                        // call edit button method 
                        editMethod(i);
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

    private void BackdropElement(int i)
    {
        Backdrops[i] = EditorGUILayout.ObjectField("Name: "+Backdrops[i].name, Backdrops[i], typeof(GameObject), true) as GameObject;
    }

    private void LayerElement(int i)
    {
        layers[i] = EditorGUILayout.ObjectField("Name: "+layers[i].name, layers[i], typeof(GameObject), true) as GameObject;
    }

    private void BackdropEditButton(int i)
    {
        selectedBackdropIndex = i;
        SelectedBackdrop = Backdrops[i];
                        
        // set the layers list to be the layers of the selected backdrop 
        selectedLayerIndex = -1;
        SelectedLayer = null; 
        SetLayersList();
        SetFloor();
    }

    private void LayerEditButton(int i)
    {
        selectedLayerIndex = i; 
        SelectedLayer = layers[i];
    }

    private void SetLayersList()
    {
        layers = SelectedBackdrop.GetComponent<Backdrop>().Layers; 
    }

    private void SetFloor()
    {
        SelectedFloor = SelectedBackdrop.transform
                        .GetComponentsInChildren<Transform>(true)
                        .FirstOrDefault(child => child.CompareTag("BackdropFloor"))
                        ?.gameObject; 
    }
}