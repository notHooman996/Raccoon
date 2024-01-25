using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GraphCreator : EditorWindow
{
    private Vector2 scrollPosition;
    private bool showWalkableList = false;
    private bool showColliderList = false; 
    private bool showVerticesList = false; 
    
    private string folderPath = "Assets/DataFiles";
    private string selectedJSONFile; 
    private string fileName;
    
    private GameObject newGameObject;
    private static List<GameObject> walkableObjects = new List<GameObject>();
    private static List<GameObject> colliderObjects = new List<GameObject>();

    private static List<Vertex> vertices = new List<Vertex>();
    private static List<Vertex> wrongVertices = new List<Vertex>();
    private static List<Edge> edges = new List<Edge>();

    private static Vertex chosenVertex;
    private bool canAddVertex = false;
    private GraphGenerator graphGenerator; 

    public static List<GameObject> GetWalkableList()
    {
        return walkableObjects; 
    }

    public static List<GameObject> GetColliderList()
    {
        return colliderObjects; 
    }
    
    public static List<Vertex> GetVertices()
    {
        return vertices; 
    }

    public static Vertex GetChosenVertex()
    {
        return chosenVertex; 
    }

    public static List<Vertex> GetWrongVertices()
    {
        return wrongVertices; 
    }

    public static List<Edge> GetEdges()
    {
        return edges;
    }
    
    [MenuItem("Window/Graph Creator")] // add it to the Window menu 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GraphCreator));
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        if (graphGenerator == null)
        {
            graphGenerator = new GraphGenerator(); 
        }
        
        SelectFile();
        DrawHorizontalLine();
        AddGameObject();
        ListGameObjectView("Walkable", ref showWalkableList, walkableObjects);
        ListGameObjectView("Collider", ref showColliderList, colliderObjects);
        DrawHorizontalLine();
        GenerateGraph();
        DrawHorizontalLine();
        SaveGraph();
        
        EditorGUILayout.EndScrollView();
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
        EditorGUILayout.LabelField("Select JSON File", EditorStyles.boldLabel);

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
                // remember to clear all data of vertices and edges 
                vertices.Clear();
                wrongVertices.Clear();
                edges.Clear();
                
                FileHandler fileHandler = new FileHandler();
                vertices = fileHandler.LoadFile(selectedJSONFile);
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

    private void ListGameObjectView(string objectType, ref bool showList, List<GameObject> objects)
    {
        showList = EditorGUILayout.Foldout(showList, $"{objectType} GameObject List");
        if (showList)
        {
            if (objects.Count > 0)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    objects[i] =
                        EditorGUILayout.ObjectField("Element " + i, objects[i], typeof(GameObject), true) as
                            GameObject;

                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(70)))
                    {
                        objects.RemoveAt(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Label("No GameObjects added yet.");
            }
        }
    }

    private void GenerateGraph()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("Generate graph", EditorStyles.boldLabel);

        if (canAddVertex)
        {
            // show this button when adding vertices 
            if (GUILayout.Button("Stop adding vertex"))
            {
                canAddVertex = false; 
                // disable adding vertices 
                SceneView.duringSceneGui -= (sceneView) => graphGenerator.CreateVertex(sceneView, vertices);
            }
        }
        else
        {
            // show this button when not adding vertices 
            if (GUILayout.Button("Add Vertex in scene"))
            {
                canAddVertex = true; 
                // enable adding vertices 
                SceneView.duringSceneGui += (sceneView) => graphGenerator.CreateVertex(sceneView, vertices);
            }
        }

        showVerticesList = EditorGUILayout.Foldout(showVerticesList, "Vertices List");
        if (showVerticesList)
        {
            if (GUILayout.Button("Disable Show Vertex"))
            {
                chosenVertex = null; 
            }
            
            GUILayout.Label("List of Vertices: ", EditorStyles.boldLabel);
            if (vertices.Count > 0)
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    VisualVertexList(i);
                }
            }
            else
            {
                GUILayout.Label("No vertices added yet.");
            }
        }

        if (GUILayout.Button("Check vertices"))
        {
            Debug.Log("Check vertices");
            wrongVertices = graphGenerator.CheckVertices();
        }

        if (GUILayout.Button("Remove all wrong vertices"))
        {
            Debug.Log("Removed all wrong vertices");
            vertices.RemoveAll(wrongVertex => wrongVertices.Any(vertex =>
                vertex.name == wrongVertex.name && vertex.position == wrongVertex.position));
            
            wrongVertices.Clear();
        }

        if (GUILayout.Button("Generate edges"))
        {
            Debug.Log("Generate edges");
            edges = graphGenerator.GenerateEdges();
        }
    }

    private void VisualVertexList(int i)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
                
        EditorGUILayout.LabelField("Vertex"+i, GUILayout.Width(50));
                
        if (GUILayout.Button("Show", GUILayout.MaxWidth(70)))
        {
            chosenVertex = vertices[i];
            Debug.Log("Show vertex " + i); 
        }
                
        if (GUILayout.Button("Remove", GUILayout.MaxWidth(70)))
        {
            vertices.RemoveAt(i);
        }
                
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
                
        EditorGUILayout.LabelField("Name: ", GUILayout.Width(50));
        EditorGUILayout.LabelField(vertices[i].name, GUILayout.Width(50));
                
        EditorGUILayout.LabelField("Position: ", GUILayout.Width(60));
        EditorGUILayout.LabelField(vertices[i].position.x.ToString("F2") +", "+ vertices[i].position.y.ToString("F2") +", "+ vertices[i].position.z.ToString("F2"));
                
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    
    private void SaveGraph()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("Save Graph", EditorStyles.boldLabel);
        
        GUILayout.Label("Enter File Name: ");
        fileName = EditorGUILayout.TextField(fileName);
        
        if (GUILayout.Button("Save as New File"))
        {
            FileHandler fileHandler = new FileHandler();
            fileHandler.SaveNewFile(fileName);
        }
        
        if (GUILayout.Button("Overwrite File"))
        {
            // Display a popup dialog with "OK" and "Cancel" buttons
            int result = EditorUtility.DisplayDialogComplex("Overwrite Graph file",
                $"You are about to overwrite the file: {selectedJSONFile}.", "Overwrite", "Cancel", "");

            // Check the result and perform actions accordingly
            switch (result)
            {
                case 0: // OK button clicked
                    FileHandler fileHandler = new FileHandler();
                    fileHandler.OverwriteFile(selectedJSONFile);
                    break;

                case 1: // Cancel button clicked
                    break;

                default:
                    // This will not be reached as there are only two buttons
                    break;
            }
        }
    }
}