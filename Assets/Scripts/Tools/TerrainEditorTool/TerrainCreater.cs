using UnityEditor;
using UnityEngine;

public class TerrainCreater : EditorWindow
{
    private GameObject ground;
    
    [MenuItem("Tools/Terrain Creater")] // add it to the Window menu 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(OldBackdropCreater));
    }

    private void OnGUI()
    {
        LoadStage();
        
        CreateGround();
    }

    private void LoadStage()
    {
        // load the ground object 
        ground = GameObject.FindGameObjectWithTag("Ground");
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
}