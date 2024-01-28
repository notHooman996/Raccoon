using System;
using UnityEditor;
using UnityEngine;

public class BackdropTool : EditorWindow
{
    private BackdropLoad backdropLoad; 
    private BackdropView backdropView;
    private BackdropCreater backdropCreater;
    private BackdropSelect backdropSelect; 
    private BackdropEditor backdropEditor;
    private BackdropLayerEditor backdropLayerEditor; 
    
    private int selectedToolTab = 0;
    private string[] toolTabNames = {"View", "Create", "Select", "Edit"};
    private int selectedEditTab = 0;
    private string[] editTabNames = { "Backdrop", "Floor", "Layer" };

    private Color selectedTabColor = Color.green;
    private Color deselectedTabColor = Color.white;
    private Color unavailableTabColor = Color.gray;

    [MenuItem("Tools/Backdrop Tool")]
    public static void ShowWindow()
    {
        GetWindow<BackdropTool>("Backdrop Tool");
    }

    private void OnEnable()
    {
        backdropLoad = ScriptableObject.CreateInstance<BackdropLoad>();
        backdropView = ScriptableObject.CreateInstance<BackdropView>();
        backdropCreater = ScriptableObject.CreateInstance<BackdropCreater>();
        backdropSelect = ScriptableObject.CreateInstance<BackdropSelect>();
        backdropEditor = ScriptableObject.CreateInstance<BackdropEditor>();
        backdropLayerEditor = ScriptableObject.CreateInstance<BackdropLayerEditor>();
    }

    private void OnGUI()
    {
        backdropLoad.Load();
        
        DrawTabs(ref selectedToolTab, toolTabNames);
        DrawContent();
    }
    
    private void DrawTabs(ref int selectedTab, string[] tabNames)
    {
        GUIStyle tabStyle = new GUIStyle(GUI.skin.button);
        tabStyle.padding = new RectOffset(10, 10, 5, 5);
        
        GUILayout.BeginHorizontal("Box");

        for (int i = 0; i < tabNames.Length; i++)
        {
            GUI.backgroundColor = i == selectedTab ? selectedTabColor : deselectedTabColor;

            if (GUILayout.Button(tabNames[i], tabStyle))
            {
                selectedTab = i;
            }

            GUI.backgroundColor = Color.white; 
        }
        
        GUILayout.EndHorizontal();
    }

    private void DrawContent()
    {
        GUILayout.Space(10);

        switch (selectedToolTab)
        {
            case 0: // view 
                
                break; 
            case 1: // create 
                backdropCreater.DrawCreateTab();
                break; 
            case 2: // select
                backdropSelect.DrawSelectTab();
                break; 
            case 3: // edit 
                DrawTabs(ref selectedEditTab, editTabNames);
                DrawEditContent();
                break; 
        }
    }

    private void DrawEditContent()
    {
        GUILayout.Space(10);

        switch (selectedEditTab)
        {
            case 0: // backdrop 
                backdropEditor.DrawEditTab();
                break; 
            case 1: // floor
                
                break; 
            case 2: // layer
                backdropLayerEditor.DrawLayerEditor();
                break; 
        }
    }
    
    public static void DrawHorizontalLine()
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
}