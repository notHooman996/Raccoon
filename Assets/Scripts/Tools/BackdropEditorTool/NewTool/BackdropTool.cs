using System;
using UnityEditor;
using UnityEngine;

public class BackdropTool : EditorWindow
{
    private BackdropCreater backdropCreater;
    private BackdropSelect backdropSelect;
    private BackdropView backdropView;
    private BackdropEditor backdropEditor;
    private BackdropLayerEditor backdropLayerEditor; 
    private BackdropLoad backdropLoad; 
    
    private int selectedEditTab = 0;
    private string[] editTabNames = { "Edit Backdrop", "Edit Layer" };
    
    [MenuItem("Tools/Backdrop Tool")]
    public static void ShowWindow()
    {
        GetWindow<BackdropTool>("Backdrop Tool");
    }

    private void OnEnable()
    {
        backdropCreater = ScriptableObject.CreateInstance<BackdropCreater>();
        backdropSelect = ScriptableObject.CreateInstance<BackdropSelect>();
        backdropView = ScriptableObject.CreateInstance<BackdropView>();
        backdropEditor = ScriptableObject.CreateInstance<BackdropEditor>();
        backdropLayerEditor = ScriptableObject.CreateInstance<BackdropLayerEditor>();
        backdropLoad = ScriptableObject.CreateInstance<BackdropLoad>();
    }

    private void OnGUI()
    {
        backdropLoad.Load();
        
        BackdropToolUtilities.DrawHorizontalLine();
        backdropView.DrawView();
        BackdropToolUtilities.DrawHorizontalLine();
        backdropCreater.DrawCreater();
        BackdropToolUtilities.DrawHorizontalLine();
        backdropSelect.DrawSelect();
        BackdropToolUtilities.DrawHorizontalLine();
        DrawTabs(ref selectedEditTab, editTabNames);
        DrawEditContent();
        BackdropToolUtilities.DrawHorizontalLine();
    }
    
    private void DrawTabs(ref int selectedTab, string[] tabNames)
    {
        GUIStyle tabStyle = new GUIStyle(GUI.skin.button);
        tabStyle.padding = new RectOffset(10, 10, 5, 5);
        
        GUILayout.BeginHorizontal("Box");

        for (int i = 0; i < tabNames.Length; i++)
        {
            GUI.backgroundColor = i == selectedTab ? BackdropToolUtilities.SelectedColor : BackdropToolUtilities.DefaultColor;

            if (GUILayout.Button(tabNames[i], tabStyle))
            {
                selectedTab = i;
            }

            GUI.backgroundColor = Color.white; 
        }
        
        GUILayout.EndHorizontal();
    }
    
    private void DrawEditContent()
    {
        switch (selectedEditTab)
        {
            case 0: // backdrop 
                backdropEditor.DrawEditor();
                break; 
            case 1: // layer
                backdropLayerEditor.DrawEditor();
                break; 
        }
    }
}