using UnityEditor;
using UnityEngine;

public class BackdropLayerEditor : EditorWindow
{
    private int layerId;
    private float layerSpacing; 
    
    public void DrawLayerEditor()
    {
        BackdropTool.DrawHorizontalLine();

        if (BackdropSelect.SelectedLayer == null)
        {
            GUILayout.Label("Choose layer from list to edit.");
        }
        else
        {
            ShowInfo();
        }
        
        BackdropTool.DrawHorizontalLine();

        EditSpacing();
        
        BackdropTool.DrawHorizontalLine();
    }

    private void ShowInfo()
    {
        EditorGUILayout.LabelField("Layer Info", EditorStyles.boldLabel);

        // load data from layer component 
        BackdropLayer backdropLayer = BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>();
        layerId = backdropLayer.LayerID;
        layerSpacing = backdropLayer.LayerSpacing; 
        
        // show the data 
        EditorGUILayout.LabelField("Layer ID: ", layerId.ToString());
        EditorGUILayout.LabelField("Layer Spacing: ", layerSpacing.ToString());
    }

    private void EditSpacing()
    {
        EditorGUILayout.LabelField("Layer Info", EditorStyles.boldLabel);

        // load data from layer component 
        BackdropLayer backdropLayer = BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>();

        backdropLayer.LayerSpacing = EditorGUILayout.FloatField("Edit layer spacing: ", backdropLayer.LayerSpacing); 
    }

    private void AddSprite()
    {
        
    }
}