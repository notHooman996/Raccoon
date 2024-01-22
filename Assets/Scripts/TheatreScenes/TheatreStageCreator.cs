using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TheatreStageCreator : EditorWindow
{
    private Vector2 scrollPosition;

    public GameObject backdropPrefab; 
    private GameObject ground;
    private List<GameObject> backdrops = new List<GameObject>();
    
    [MenuItem("Window/Theatre Stage Editor")] // add it to the Window menu 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TheatreStageCreator));
    }

    private void OnGUI()
    {
        LoadStage();
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        CreateGround();
        DrawHorizontalLine();
        SetBackdropPrefab();
        AddBackdrop();
        
        EditorGUILayout.EndScrollView();
    }
    
    private void DrawHorizontalLine()
    {
        GUILayout.Space(10);
        
        Color color = Color.gray;
        int thickness = 1;
        int padding = 10; 
        
        Rect rect = EditorGUILayout.GetControlRect(false, thickness, EditorStyles.helpBox);
        rect.height = thickness;
        rect.y += padding / 2;
        rect.x -= 2;
        rect.width += 6;
        EditorGUI.DrawRect(rect, color);
        
        GUILayout.Space(10);
    }
    
    private void LoadStage()
    {
        // load the ground object 
        ground = GameObject.FindGameObjectWithTag("Ground");

        // load the backdrops 
        backdrops = GameObject.FindGameObjectsWithTag("Backdrop").ToList();
    }

    private void CreateGround()
    {
        // TODO - make separate editor for ground/water 
        
        EditorGUILayout.LabelField("Create ground");
        
        // add plane object 
        // set tag as Ground
        // name the object 

        if (GUILayout.Button("Create"))
        {
            ground = new GameObject("Ground");

            ground.tag = "Ground";
        }
    }
    
    private void SetBackdropPrefab()
    {
        GUILayout.Label("Prefab Setter", EditorStyles.boldLabel);
        
        // Display the stored prefab reference 
        backdropPrefab = (GameObject)EditorGUILayout.ObjectField("BackdropPrefab", backdropPrefab, typeof(GameObject), false);
    }

    private void AddBackdrop()
    {
        EditorGUILayout.LabelField("Add backdrop");
        
        // use prefab 
        if (GUILayout.Button("Add Backdrop"))
        {
            // new gameobject 
            GameObject newBackdrop = PrefabUtility.InstantiatePrefab(backdropPrefab) as GameObject;
        }
    }

    private void EditBackdrop()
    {
        EditorGUILayout.LabelField("Choose backdrop");
        
        
        
        
        EditorGUILayout.LabelField("Edit backdrop");
        
        // edit number of layers (give the layers ids)
        // edit width between each layer 
        // entire backdrop can be moved (x,y,z)
        // entire backdrop can be rotated (remember, only visible from one side)
        // add elements to a layer 
            // choose layer first 
    }
    
    
}
