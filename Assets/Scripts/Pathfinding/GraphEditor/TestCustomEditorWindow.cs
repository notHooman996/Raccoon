using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestCustomEditorWindow : EditorWindow
{
    private string selectedJSONFile; 
    private string folderPath = "Assets/DataFiles";
    
    private List<GameObject> walkableObjects = new List<GameObject>();
    private List<GameObject> colliderObjects = new List<GameObject>();
    private GameObject newGameObject;

    private List<Vertex> vertices = new List<Vertex>();
    
    private string fileName; 
    
    
    [MenuItem("Window/Custom Editor Window")] // add it to the Window menu 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TestCustomEditorWindow));
    }

    void OnGUI()
    {
        SelectFile();
        DrawHorizontalLine();
        AddGameObject();
        WalkableList();
        ColliderList();
        DrawHorizontalLine();
        CreateGraph();
        DrawHorizontalLine();
        SaveGraph();
    }

    private void DrawHorizontalLine()
    {
        Color color = Color.gray;
        int thickness = 1;
        int padding = 10; 
        
        Rect rect = EditorGUILayout.GetControlRect(false, thickness, EditorStyles.helpBox);
        rect.height = thickness;
        rect.y += padding / 2;
        rect.x -= 2;
        rect.width += 6;
        EditorGUI.DrawRect(rect, color);
    }

    private void SelectFile()
    {
        EditorGUILayout.LabelField("Select JSON File");

        List<string> jsonFiles = GetJSONFilesInFolder(folderPath);

        if (jsonFiles.Count > 0)
        {
            int selectedIndex = jsonFiles.IndexOf(selectedJSONFile);
            selectedIndex = Mathf.Max(0, selectedIndex); // ensure index is not negative 
            selectedIndex = EditorGUILayout.Popup(selectedIndex, jsonFiles.ToArray());

            selectedJSONFile = jsonFiles[selectedIndex];
        }
        else
        {
            EditorGUILayout.LabelField("No JSON files found in folder.");
        }

        if (GUILayout.Button("Load Selected JSON"))
        {
            if (!string.IsNullOrEmpty(selectedJSONFile))
            {
                string filePath = System.IO.Path.Combine(folderPath, selectedJSONFile);
                string jsonContent = File.ReadAllText(filePath);
                // TODO - process loaded JSON data 
            }
            else
            {
                Debug.Log("Please select a JSON file.");
            }
        }
    }

    private List<string> GetJSONFilesInFolder(string folder)
    {
        List<string> jsonFiles = new List<string>();
    
        if (Directory.Exists(folder))
        {
            string[] files = Directory.GetFiles(folder, "*.json");
    
            foreach (string file in files)
            {
                jsonFiles.Add(System.IO.Path.GetFileName(file));
            }
        }
    
        return jsonFiles; 
    }

    private void AddGameObject()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("Add GameObject to a List", EditorStyles.boldLabel);
        
        newGameObject = EditorGUILayout.ObjectField("New GameObject: ", newGameObject, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Add GameObject as WalkableObject") && newGameObject != null)
        {
            walkableObjects.Add(newGameObject);
            newGameObject = null; 
        }
        
        if (GUILayout.Button("Add GameObject as CollisionObject") && newGameObject != null)
        {
            colliderObjects.Add(newGameObject);
            newGameObject = null; 
        }
    }

    private void WalkableList()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("List of Walkable GameObjects: ", EditorStyles.boldLabel);

        if (walkableObjects.Count > 0)
        {
            for (int i = 0; i < walkableObjects.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                walkableObjects[i] = EditorGUILayout.ObjectField("Element "+i, walkableObjects[i], typeof(GameObject), true) as GameObject;

                if (GUILayout.Button("Remove", GUILayout.MaxWidth(70)))
                {
                    walkableObjects.RemoveAt(i);
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.Label("No GameObjects added yet.");
        }
    }
    
    private void ColliderList()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("List of Collider GameObjects: ", EditorStyles.boldLabel);

        if (colliderObjects.Count > 0)
        {
            for (int i = 0; i < colliderObjects.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                colliderObjects[i] = EditorGUILayout.ObjectField("Element "+i, colliderObjects[i], typeof(GameObject), true) as GameObject;

                if (GUILayout.Button("Remove", GUILayout.MaxWidth(70)))
                {
                    colliderObjects.RemoveAt(i);
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.Label("No GameObjects added yet.");
        }
    }

    private void CreateGraph()
    {
        GUILayout.Space(10);
        
        if (GUILayout.Button("Generate Graph"))
        {
            Debug.Log("Generate Graph");
            GraphGenerator.Generate(walkableObjects, colliderObjects);
        }
        
        if (GUILayout.Button("Draw Gizmos"))
        {
            Debug.Log("Draw gizmos");
            SceneView.RepaintAll();
        }
    }

    private void SaveGraph()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("Save Graph", EditorStyles.boldLabel);
        
        GUILayout.Label("Enter File Name: ");
        fileName = EditorGUILayout.TextField(fileName);
        
        if (GUILayout.Button("Save As"))
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = System.IO.Path.Combine(folderPath, fileName + ".json");

                if (File.Exists(filePath))
                {
                    Debug.Log("File name already exist.");
                }
                else
                {
                    SaveToFile(filePath);
                }
            }
            else
            {
                Debug.Log("Please enter a file name.");
            }
        }
        
        
        if (GUILayout.Button("Overwrite"))
        {
            string filePath = System.IO.Path.Combine(folderPath, selectedJSONFile);
            
            SaveToFile(filePath);
        }
    }

    private void SaveToFile(string path)
    {
        // TODO - perform logic here 
        File.WriteAllText(path, "");
    }
}
