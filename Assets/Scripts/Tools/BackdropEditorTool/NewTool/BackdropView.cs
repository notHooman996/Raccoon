using UnityEditor;
using UnityEngine;

public class BackdropView : EditorWindow
{
    public void DrawView()
    {
        EditorGUILayout.BeginVertical("box");
        
        EditorGUILayout.LabelField("View options: ", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        
        ViewAll();
        ViewSelectedBackdrop();
        ViewSelectedLayer();
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        FocusBackdrop();
        FocusLayer();
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void ViewAll()
    {
        // TODO 
        // on/off switch 
        // show/hide all backdrops 
        if (GUILayout.Button("Show all"))
        {
            
        }
    }

    private void ViewSelectedBackdrop()
    {
        // TODO 
        // only show the selected backdrop 
        // hide all other backdrops 
        if (GUILayout.Button("Show Backdrop"))
        {
            
        }
    }

    private void ViewSelectedLayer()
    {
        // TODO 
        // only show the selected layer 
        // hide all other layers 
        if (GUILayout.Button("Show Layer"))
        {
            
        }
    }

    private void FocusBackdrop()
    {
        // TODO 
        // focus the sceneview to look at selected backdrop 
        if (GUILayout.Button("Focus Backdrop"))
        {
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                // Set the scene view pivot to the position of the selected object
                sceneView.LookAt(BackdropSelect.SelectedBackdrop.transform.position, sceneView.rotation);
                    
                // repaint the scene view to reflect the changes 
                sceneView.Repaint();
            }
        }
    }
    
    private void FocusLayer()
    {
        // TODO 
        // focus the sceneview to look at selected layer, use the layer face normal 
        if (GUILayout.Button("Focus Layer"))
        {
            
        }
    }
}