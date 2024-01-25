using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TheatreStageCreator : EditorWindow
{
    private Vector2 scrollPosition;

    public GameObject backdropPrefab; 
    private GameObject ground;
    private GameObject backdropHolderObject; 
    private List<GameObject> backdrops = new List<GameObject>();
    private GameObject selectedBackdrop = null; 
    private bool showBackdropsList = false;
    private bool showEditBackdropTransform = false;
    
    [MenuItem("Window/Theatre Stage Editor")] // add it to the Window menu 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TheatreStageCreator));
    }

    private void OnGUI()
    {
        LoadStage();
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        //CreateGround();
        DrawHorizontalLine();
        AddBackdrop();
        DrawHorizontalLine();
        BackdropList();
        DrawHorizontalLine();
        EditMenu();
        DrawHorizontalLine();
        
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

        // load backdrop holder 
        backdropHolderObject = GameObject.FindGameObjectWithTag("BackdropHolder");
        
        // load the backdrops 
        backdrops = GameObject.FindGameObjectsWithTag("Backdrop").ToList();
    }

    private void CreateGround()
    {
        // TODO - make separate editor for ground/water 
        
        EditorGUILayout.LabelField("Create ground", EditorStyles.boldLabel);
        
        // add plane object 
        // set tag as Ground
        // name the object 

        if (GUILayout.Button("Create"))
        {
            ground = new GameObject("Ground");

            ground.tag = "Ground";
        }
    }
    
    private void AddBackdrop()
    {
        EditorGUILayout.LabelField("Add backdrop", EditorStyles.boldLabel);
        
        SetBackdropPrefab();
        
        // use prefab 
        if (GUILayout.Button("Add Backdrop"))
        {
            if (backdropHolderObject == null)
            {
                // new backdrop holder object 
                backdropHolderObject = new GameObject();
                backdropHolderObject.name = "BackdropHolder";
                backdropHolderObject.tag = "BackdropHolder";
            }
            
            // new backdrop gameobject 
            GameObject newBackdrop = PrefabUtility.InstantiatePrefab(backdropPrefab) as GameObject;
            newBackdrop.transform.parent = backdropHolderObject.transform; 
            backdrops.Add(newBackdrop);
        }
    }
    
    private void SetBackdropPrefab()
    {
        // Display the stored prefab reference 
        backdropPrefab = (GameObject)EditorGUILayout.ObjectField("BackdropPrefab", backdropPrefab, typeof(GameObject), false);
    }

    private void BackdropList()
    {
        showBackdropsList = EditorGUILayout.Foldout(showBackdropsList, "Backdrop GameObject List");
        if (showBackdropsList)
        {
            if (backdrops.Count > 0)
            {
                for (int i = 0; i < backdrops.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    backdrops[i] = EditorGUILayout.ObjectField("Name: "+backdrops[i].name, backdrops[i], typeof(GameObject), true) as GameObject;

                    if (GUILayout.Button("Edit", GUILayout.MaxWidth(70)))
                    {
                        selectedBackdrop = backdrops[i];
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

    private void EditMenu()
    {
        EditorGUILayout.LabelField("Edit backdrop", EditorStyles.boldLabel);

        if (selectedBackdrop == null)
        {
            GUILayout.Label("Choose backdrop from list to edit.");
        }
        else
        {
            if (GUILayout.Button("Stop edit", GUILayout.MaxWidth(70)))
            {
                selectedBackdrop = null;
                return; 
            }
            
            if (GUILayout.Button("Focus Backdrop", GUILayout.MaxWidth(120)))
            {
                SceneView sceneView = SceneView.lastActiveSceneView;
                if (sceneView != null)
                {
                    // Set the scene view pivot to the position of the selected object
                    sceneView.LookAt(selectedBackdrop.transform.position, sceneView.rotation);
                    
                    // repaint the scene view to reflect the changes 
                    sceneView.Repaint();
                }
            }

            EditBackdrop();
            EditFloor();
            EditLayer();
        }
    }

    private void EditBackdrop()
    {
        // edit the chosen backdrop 
        EditorGUILayout.ObjectField("Name: "+selectedBackdrop.name, selectedBackdrop, typeof(GameObject), true);

        EditBackdropName();
        EditBackdropTransform();
    }

    private void EditBackdropName()
    {
        // change name 
        string newName = selectedBackdrop.name; 
        newName = EditorGUILayout.TextField("Rename backdrop: ", newName);
        selectedBackdrop.name = newName; 
    }

    private void EditBackdropTransform()
    {
        showEditBackdropTransform = EditorGUILayout.Foldout(showEditBackdropTransform, "Transform");
        if (showEditBackdropTransform)
        {
            EditorGUI.BeginChangeCheck();
            
            // Display position fields
            selectedBackdrop.transform.position = EditorGUILayout.Vector3Field("Position", selectedBackdrop.transform.position);
            
            // Display rotation fields 
            selectedBackdrop.transform.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", selectedBackdrop.transform.rotation.eulerAngles));
            
            // Display scale fields
            selectedBackdrop.transform.localScale = EditorGUILayout.Vector3Field("Scale", selectedBackdrop.transform.localScale);
            
            // Check if any changes were made
            if (EditorGUI.EndChangeCheck())
            {
                // Mark the object as dirty to apply the changes
                EditorUtility.SetDirty(selectedBackdrop);
            }
        }
    }

    private void EditFloor()
    {
        
    }

    private void EditLayer()
    {
        // edit number of layers (give the layers ids)
        // edit width between each layer 

        // add elements to a layer 
            // choose layer first 
    }
}
