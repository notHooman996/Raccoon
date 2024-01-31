using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackdropEditor : EditorWindow
{
    private Vector2 scrollPosition;
    
    public void DrawEditor()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        if (BackdropSelect.SelectedBackdrop != null)
        {
            DrawList();
        }
        else
        {
            EditorGUILayout.LabelField("Select a backdrop to edit.");
        }
        
        EditorGUILayout.EndScrollView();
    }
    
    private void DrawList()
    {
        EditorGUILayout.LabelField("Backdrop Layers: ");
        
        if (BackdropLoad.Layers.Count > 0)
        {
            for (int i = 0; i < BackdropLoad.Layers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                LayerElement(i);
                
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select a layer to edit.");
        }
    }
    
    private void LayerElement(int i)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Name: "+BackdropLoad.Layers[i].name);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        
        BackdropLayer layerComponent = BackdropLoad.Layers[i].GetComponent<BackdropLayer>();
        
        EditorGUILayout.LabelField("ID: ", GUILayout.Width(30));
        EditorGUILayout.LabelField(layerComponent.LayerID.ToString(), GUILayout.Width(50));
        
        EditorGUILayout.LabelField("Spacing: ", GUILayout.Width(50));
        layerComponent.LayerSpacing = EditorGUILayout.FloatField(layerComponent.LayerSpacing);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}