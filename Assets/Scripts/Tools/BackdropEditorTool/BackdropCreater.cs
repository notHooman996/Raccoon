using UnityEditor;
using UnityEngine;

public class BackdropCreater : EditorWindow
{
    private Vector2 scrollPosition;

    public void DrawCreateTab()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        BackdropTool.DrawHorizontalLine();
        AddBackdrop();
        BackdropTool.DrawHorizontalLine();
        AddLayer();
        BackdropTool.DrawHorizontalLine();
        
        EditorGUILayout.EndScrollView();
    }
    
    private void AddBackdrop()
    {
        EditorGUILayout.LabelField("Add backdrop", EditorStyles.boldLabel);
        
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
            GameObject newBackdrop = PrefabUtility.InstantiatePrefab(BackdropLoad.BackdropPrefab) as GameObject;
            newBackdrop.transform.parent = BackdropLoad.BackdropHolderObject.transform; 
            BackdropSelect.Backdrops.Add(newBackdrop);
        }
    }
    
    private void AddLayer()
    {
        EditorGUILayout.LabelField("Add layer", EditorStyles.boldLabel);
        
        if (BackdropSelect.SelectedBackdrop == null)
        {
            GUILayout.Label("Choose backdrop from list.");
        }
        else
        {
            // use prefab 
            if (GUILayout.Button("Add Layer"))
            {
                // new layer gameobject 
                GameObject newLayer = PrefabUtility.InstantiatePrefab(BackdropLoad.LayerPrefab) as GameObject;
                newLayer.transform.parent = BackdropSelect.SelectedBackdrop.transform; 
                // figure out the id 
                newLayer.GetComponent<BackdropLayer>().LayerID =
                    BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().GetHighestLayerId() + 1; 
                // set initial spacing 
                newLayer.GetComponent<BackdropLayer>().LayerSpacing = 0.5f; 
                BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().AddLayer(newLayer);
            }
        }
    }
}