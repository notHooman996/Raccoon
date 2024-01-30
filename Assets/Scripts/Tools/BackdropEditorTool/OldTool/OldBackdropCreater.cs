using UnityEditor;
using UnityEngine;

public class OldBackdropCreater : EditorWindow
{
    private Vector2 scrollPosition;

    public void DrawCreateTab()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        OldBackdropTool.DrawHorizontalLine();
        AddBackdrop();
        OldBackdropTool.DrawHorizontalLine();
        AddLayer();
        OldBackdropTool.DrawHorizontalLine();
        
        EditorGUILayout.EndScrollView();
    }
    
    private void AddBackdrop()
    {
        EditorGUILayout.LabelField("Add backdrop", EditorStyles.boldLabel);
        
        // use prefab 
        if (GUILayout.Button("Add Backdrop"))
        {
            if (OldBackdropLoad.BackdropHolderObject == null)
            {
                // new backdrop holder object 
                OldBackdropLoad.BackdropHolderObject = new GameObject();
                OldBackdropLoad.BackdropHolderObject.name = "BackdropHolder";
                OldBackdropLoad.BackdropHolderObject.tag = "BackdropHolder";
            }
            
            // new backdrop gameobject 
            GameObject newBackdrop = Instantiate(OldBackdropLoad.BackdropPrefab);
            newBackdrop.transform.parent = OldBackdropLoad.BackdropHolderObject.transform;
            newBackdrop.AddComponent<Backdrop>();
            OldBackdropSelect.Backdrops.Add(newBackdrop);
        }
    }
    
    private void AddLayer()
    {
        EditorGUILayout.LabelField("Add layer", EditorStyles.boldLabel);
        
        if (OldBackdropSelect.SelectedBackdrop == null)
        {
            GUILayout.Label("Choose backdrop from list.");
        }
        else
        {
            // use prefab 
            if (GUILayout.Button("Add Layer"))
            {
                // new layer gameobject 
                GameObject newLayer = Instantiate(OldBackdropLoad.LayerPrefab);
                newLayer.transform.parent = OldBackdropSelect.SelectedBackdrop.transform; 
                // add component 
                newLayer.AddComponent<BackdropLayer>();
                // figure out the id 
                newLayer.GetComponent<BackdropLayer>().LayerID =
                    OldBackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().GetHighestLayerId() + 1; 
                // set initial spacing 
                newLayer.GetComponent<BackdropLayer>().LayerSpacing = 0.5f; 
                OldBackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().AddLayer(newLayer);
            }
        }
    }
}