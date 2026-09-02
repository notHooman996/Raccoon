using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class DevSettingsTool : EditorWindow
{
    private int selectedMode = 0; 
    private int previousMode = 0;
    private string[] modes = { "Test", "Prod" };
    
    private bool animationToggle = false;

    private int selectedTime = 0;
    private string[] timeOptions = { "Default", "Option 1", "Option 2", "Option 3" }; 
    
    private int selectedScene = 0;
    private string[] sceneOptions = { "Default", "Option 1", "Option 2", "Option 3" };

    private int selectedQuality = 0; 
    private string[] qualities = { "Low", "Medium" , "High"  };    

    private string testSpritePath = "Assets/Resources/Sprites/Test";
    private string prodSpritePath = "Assets/Resources/Sprites/Prod";
    private string loadSpritePath; 
    
    [MenuItem("Tools/DevSettingsTool")]
    public static void ShowWindow()
    {
        GetWindow<DevSettingsTool>("DevSettings Tool");
    }
    
    private void OnEnable()
    {}

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal(); 
        selectedMode = GUILayout.Toolbar(selectedMode, modes);
        if (selectedMode != previousMode)
        {
            loadMode();
            previousMode = selectedMode;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        
        drawOptions(); 
    }

    private void DrawTabs()
    {
        
    }

    private void DrawEditContent()
    {
        
    }

    private void resetDefault()
    {
        animationToggle = false;
        selectedQuality = 0; 
        selectedTime = 0;
        selectedScene = 0;
    }

    private void loadMode()
    {
        resetDefault(); 
        if (selectedMode==0)
        {
            Debug.Log("Test mode");
            // TODO - change load path 
            loadSpritePath = testSpritePath; 
        }
        else if (selectedMode == 1)
        {
            Debug.Log("Prod mode");
            // TODO - change load path 
            loadSpritePath = prodSpritePath; 
        }
    }

    private void drawOptions()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.BeginVertical("box");
        if (GUILayout.Button("Reset to default"))
        {
            resetDefault(); 
        }
        EditorGUILayout.EndVertical(); 
        
        EditorGUILayout.BeginVertical("box");
        selectedTime = EditorGUILayout.Popup("Time", selectedTime, timeOptions);
        
        selectedScene = EditorGUILayout.Popup("Scene", selectedScene, sceneOptions);
        
        selectedQuality = EditorGUILayout.Popup("Quality",selectedQuality, qualities);
        
        animationToggle = EditorGUILayout.Toggle("Animation toggle", animationToggle);
        EditorGUILayout.EndVertical(); 
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
}