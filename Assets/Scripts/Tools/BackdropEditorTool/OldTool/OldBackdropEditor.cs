using UnityEditor;
using UnityEngine;

public class OldBackdropEditor : EditorWindow
{
    private bool showEditBackdropTransform = false;

    public void DrawEditTab()
    {
        OldBackdropTool.DrawHorizontalLine();
        EditMenu();
    }
    
    private void EditMenu()
    {
        EditorGUILayout.LabelField("Edit backdrop", EditorStyles.boldLabel);

        if (OldBackdropSelect.SelectedBackdrop == null)
        {
            GUILayout.Label("Choose backdrop from list to edit.");
        }
        else
        {
            if (GUILayout.Button("Focus Backdrop", GUILayout.MaxWidth(120)))
            {
                SceneView sceneView = SceneView.lastActiveSceneView;
                if (sceneView != null)
                {
                    // Set the scene view pivot to the position of the selected object
                    sceneView.LookAt(OldBackdropSelect.SelectedBackdrop.transform.position, sceneView.rotation);
                    
                    // repaint the scene view to reflect the changes 
                    sceneView.Repaint();
                }
            }

            EditBackdrop();
        }
    }

    private void EditBackdrop()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Name: "+OldBackdropSelect.SelectedBackdrop.name, OldBackdropSelect.SelectedBackdrop, typeof(GameObject), true);
        EditorGUI.EndDisabledGroup();
        
        OldBackdropTool.DrawHorizontalLine();
        ChangeName(OldBackdropSelect.SelectedBackdrop);
        OldBackdropTool.DrawHorizontalLine();
        Transform(ref showEditBackdropTransform, OldBackdropSelect.SelectedBackdrop);
        OldBackdropTool.DrawHorizontalLine();
    }

    private void ChangeName(GameObject gameObject)
    {
        // change name 
        string newName = gameObject.name; 
        newName = EditorGUILayout.TextField("Rename: ", newName);
        gameObject.name = newName;
    }

    private void Transform(ref bool showTransform, GameObject gameObject)
    {
        showTransform = EditorGUILayout.Foldout(showTransform, "Transform");
        if (showTransform)
        {
            EditorGUI.BeginChangeCheck();
            
            // Display position fields
            gameObject.transform.position = EditorGUILayout.Vector3Field("Position", gameObject.transform.position);
            
            // Display rotation fields 
            gameObject.transform.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", gameObject.transform.rotation.eulerAngles));
            
            // Display scale fields
            gameObject.transform.localScale = EditorGUILayout.Vector3Field("Scale", gameObject.transform.localScale);
            
            // Check if any changes were made
            if (EditorGUI.EndChangeCheck())
            {
                // Mark the object as dirty to apply the changes
                EditorUtility.SetDirty(gameObject);
            }
        }
    }

    private void EditFloor()
    {
        
    }

    private void EditLayer()
    {
        // edit number of layers (give the layers ids)
        // edit width between each layer 

        // add elements to a layer 
            // choose layer first 
    }
}