using UnityEditor;
using UnityEngine;

public class BackdropToolUtilities : EditorWindow
{
    public static Color SelectedTabColor { get; private set; } = Color.green;
    public static Color DeselectedTabColor { get; private set; } = Color.white;
    public static Color UnavailableTabColor { get; private set; } = Color.gray;
    
    public static void DrawHorizontalLine()
    {
        //GUILayout.Space(10);
        
        Color color = Color.gray;
        int thickness = 1;
        int padding = 10; 
        
        Rect rect = EditorGUILayout.GetControlRect(false, thickness, EditorStyles.helpBox);
        rect.height = thickness;
        rect.y += padding/2;
        rect.x -= 2;
        rect.width += 6;
        EditorGUI.DrawRect(rect, color);
        
        GUILayout.Space(10);
    }
}