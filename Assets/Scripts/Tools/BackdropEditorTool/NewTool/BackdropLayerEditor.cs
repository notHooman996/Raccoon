using UnityEditor;
using UnityEngine;

public class BackdropLayerEditor : EditorWindow
{
    private Vector2 scrollPosition;
    
    public void DrawEditor()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        // method call 
        
        EditorGUILayout.EndScrollView();
    }

    
}