using UnityEditor;
using UnityEngine;

public class BackdropView : EditorWindow
{
    public void DrawView()
    {
        EditorGUILayout.BeginHorizontal();
        ViewAll();
        ViewSelectedBackdrop();
        ViewSelectedLayer();
        EditorGUILayout.EndHorizontal();
    }

    private void ViewAll()
    {
        // TODO 
        // on/off switch 
        // show/hide all backdrops 
        if (GUILayout.Button("View all"))
        {
            
        }
    }

    private void ViewSelectedBackdrop()
    {
        // TODO 
        // only show the selected backdrop 
        // hide all other backdrops 
        if (GUILayout.Button("View Backdrop"))
        {
            
        }
    }

    private void ViewSelectedLayer()
    {
        // TODO 
        // only show the selected layer 
        // hide all other layers 
        if (GUILayout.Button("View Layer"))
        {
            
        }
    }
}