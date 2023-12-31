using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestCustomScript))]
public class TestCustomScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        // custum inspector code here 
        TestCustomScript script = (TestCustomScript)target; 
        
        // example: change variable in the script 
        script.customVariable = EditorGUILayout.IntField("Custom variable", script.customVariable);
        
        // save changes 
        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }
}
