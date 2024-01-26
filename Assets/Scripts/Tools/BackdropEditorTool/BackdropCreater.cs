using UnityEditor;
using UnityEngine;

public class BackdropCreater : EditorWindow
{
    private Vector2 scrollPosition;
    [SerializeField] private GameObject backdropPrefab; 

    public void DrawCreateTab()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        DrawHorizontalLine();
        AddBackdrop();
        DrawHorizontalLine();
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
    
    private void AddBackdrop()
    {
        EditorGUILayout.LabelField("Add backdrop", EditorStyles.boldLabel);
        
        SetBackdropPrefab();
        
        // use prefab 
        if (GUILayout.Button("Add Backdrop"))
        {
            if (BackdropLoad.BackdropHolderObject == null)
            {
                // new backdrop holder object 
                BackdropLoad.BackdropHolderObject = new GameObject();
                BackdropLoad.BackdropHolderObject.name = "BackdropHolder";
                BackdropLoad.BackdropHolderObject.tag = "BackdropHolder";
            }
            
            // new backdrop gameobject 
            GameObject newBackdrop = PrefabUtility.InstantiatePrefab(backdropPrefab) as GameObject;
            newBackdrop.transform.parent = BackdropLoad.BackdropHolderObject.transform; 
            BackdropSelect.Backdrops.Add(newBackdrop);
        }
    }
    
    private void SetBackdropPrefab()
    {
        // Display the stored prefab reference 
        backdropPrefab = (GameObject)EditorGUILayout.ObjectField("BackdropPrefab", backdropPrefab, typeof(GameObject), false);
    }
}