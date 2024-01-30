using UnityEditor;
using UnityEngine;

public class BackdropCreater : EditorWindow
{
    public void DrawCreater()
    {
        GUILayout.BeginHorizontal();
        AddBackdrop();
        AddLayer();
        GUILayout.EndHorizontal();
    }

    private void AddBackdrop()
    {
        if (GUILayout.Button("Add Backdrop"))
        {
            // TODO 
        }
    }

    private void AddLayer()
    {
        if (GUILayout.Button("Add Layer"))
        {
            // TODO 
        }
    }
}