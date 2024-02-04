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

        if (BackdropSelect.SelectedBackdrop != null && BackdropLoad.Layers != null)
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
            EditorGUILayout.LabelField("Add layers to the backdrop to edit.");
        }
    }
    
    private void LayerElement(int i)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Name: "+BackdropLoad.Layers[i].name);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        LayerSpacing(i);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        LayerTilt(i);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void LayerSpacing(int i)
    {
        BackdropLayer layerComponent = BackdropLoad.Layers[i].GetComponent<BackdropLayer>();
        
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("ID: ", GUILayout.Width(50));
        EditorGUILayout.LabelField(layerComponent.LayerID.ToString(), GUILayout.Width(50));
        
        EditorGUILayout.LabelField("Spacing: ", GUILayout.Width(50));
        float currentLayerSpacing = layerComponent.LayerSpacing; 
        layerComponent.LayerSpacing = EditorGUILayout.FloatField(layerComponent.LayerSpacing, GUILayout.Width(100));
        // check if change has occured 
        if (currentLayerSpacing != layerComponent.LayerSpacing)
        {
            // update the layers 
            BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().DrawLayers();
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void LayerTilt(int i)
    {
        EditorGUILayout.BeginVertical("box");
        
        EditorGUILayout.LabelField("Tilt: ", GUILayout.Width(50));
        Vector3 layerRotation = BackdropLoad.Layers[i].transform.localRotation.eulerAngles; 
        
        EditorGUILayout.BeginHorizontal();
        
        layerRotation.x = EditorGUILayout.FloatField("\tX: ", layerRotation.x);
        if (GUILayout.Button("Reset X tilt"))
        {
            layerRotation.x = 270;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        
        layerRotation.y = EditorGUILayout.FloatField("\tY: ", layerRotation.y);
        if (GUILayout.Button("Reset Y tilt"))
        {
            layerRotation.y = 0;
        }
        
        EditorGUILayout.EndHorizontal();
        
        layerRotation.z = BackdropLoad.Layers[i].transform.localRotation.eulerAngles.z; 
        BackdropLoad.Layers[i].transform.localRotation = Quaternion.Euler(layerRotation);
        
        EditorGUILayout.EndVertical();
    }
}