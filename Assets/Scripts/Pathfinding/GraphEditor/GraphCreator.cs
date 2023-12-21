using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GraphCreator : EditorWindow
{
    private Vector2 scrollPosition;
    private bool showWalkableList = false;
    private bool showColliderList = false; 
    private bool showVerticesList = false; 
    
    private string selectedJSONFile; 
    private string folderPath = "Assets/DataFiles";
    private string fileName;
    
    private GameObject newGameObject;
    private List<GameObject> walkableObjects = new List<GameObject>();
    private List<GameObject> colliderObjects = new List<GameObject>();

    private static List<Vertex> vertices = new List<Vertex>();
    private static List<Vertex> wrongVertices = new List<Vertex>();
    private static List<Edge> edges = new List<Edge>();

    private static Vertex chosenVertex;
    private bool canAddVertex = false;

    private float proximityRadius = 3; // TODO - player gameobject size radius 

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
        
        SelectFile();
        DrawHorizontalLine();
        AddGameObject();
        WalkableList();
        ColliderList();
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
        showWalkableList = EditorGUILayout.Foldout(showWalkableList, "Walkable GameObject List");
        if (showWalkableList)
        {
            GUILayout.Label("List of Walkable GameObjects: ", EditorStyles.boldLabel);

            if (walkableObjects.Count > 0)
            {
                for (int i = 0; i < walkableObjects.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    walkableObjects[i] =
                        EditorGUILayout.ObjectField("Element " + i, walkableObjects[i], typeof(GameObject), true) as
                            GameObject;

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
    }
    
    private void ColliderList()
    {
        showColliderList = EditorGUILayout.Foldout(showColliderList, "Collider GameObject List");
        if (showColliderList)
        {
            GUILayout.Label("List of Collider GameObjects: ", EditorStyles.boldLabel);

            if (colliderObjects.Count > 0)
            {
                for (int i = 0; i < colliderObjects.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    colliderObjects[i] =
                        EditorGUILayout.ObjectField("Element " + i, colliderObjects[i], typeof(GameObject), true) as
                            GameObject;

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
                SceneView.duringSceneGui -= CreateVertex;
            }
        }
        else
        {
            // show this button when not adding vertices 
            if (GUILayout.Button("Add Vertex in scene"))
            {
                canAddVertex = true; 
                // enable adding vertices 
                SceneView.duringSceneGui += CreateVertex;
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
            CheckVertices();
        }

        if (GUILayout.Button("Generate edges"))
        {
            Debug.Log("Generate edges");
            edges.Clear(); // remember to clear list before generating new edges 
            GenerateEdges();
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

    private void CreateVertex(SceneView sceneView)
    {
        // check for mouse left click 
        Event currentEvent = Event.current; 
        if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown && currentEvent.button == 0) 
        {
            // cast a ray from the mouse position in relation to the camera's view 
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // check if ray hits objects in walkable list 
                foreach (GameObject gameObject in walkableObjects)
                {
                    if (hitInfo.collider.gameObject == gameObject)
                    {
                        // create vertex at entry point 
                        vertices.Add(new Vertex("test", hitInfo.point));
                    }
                }
            }
        }
    }

    private void CheckVertices()
    {
        wrongVertices.Clear(); 
        foreach (Vertex vertex in vertices)
        {
            bool isVertexTooClose = false; 
            foreach (GameObject colliderObject in colliderObjects)
            {
                float distance = Vector3.Distance(colliderObject.transform.position, vertex.position);
                if (distance <= proximityRadius) // TODO - calculate common radius for player and collider object 
                {
                    isVertexTooClose = true;
                    break; 
                }
            }

            if (isVertexTooClose)
            {
                wrongVertices.Add(vertex);
            }
        }
    }
    
    private void GenerateEdges()
    {
        // connect all vertices with edges 
        foreach (Vertex vertex1 in vertices)
        {
            foreach (Vertex vertex2 in vertices)
            {
                // make sure not to make edges at same vertex 
                if (vertex1 != vertex2)
                {
                    bool hitAnyObject = false; 
                    
                    // define raycast start and end point
                    Vector3 startPoint = vertex1.position;
                    Vector3 endPoint = vertex2.position;

                    Ray ray = new Ray(startPoint, endPoint - startPoint);
                    RaycastHit hit; 
                    
                    // check if collider in the way 
                    foreach (GameObject colliderObject in colliderObjects)
                    {
                        if (colliderObject != null)
                        {
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.collider.gameObject == colliderObject)
                                {
                                    hitAnyObject = true;
                                    break; // exit the loop 
                                }
                            }
                        }
                    }

                    // if no collision object was hit, then create edge
                    if (!hitAnyObject)
                    {
                        CreateEdge(vertex1, vertex2);
                    }
                }
            }
        }
    }

    private void CreateEdge(Vertex from, Vertex to)
    {
        // make edges both ways 
        edges.Add(new Edge(from, to));
        edges.Add(new Edge(to, from));
    }
    
    private void SaveGraph()
    {
        GUILayout.Space(10);
        
        GUILayout.Label("Save Graph", EditorStyles.boldLabel);
        
        GUILayout.Label("Enter File Name: ");
        fileName = EditorGUILayout.TextField(fileName);
        
        if (GUILayout.Button("Save as New File"))
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = System.IO.Path.Combine(folderPath, fileName + ".json");

                if (File.Exists(filePath))
                {
                    Debug.Log("File name already exists.");
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
        
        
        if (GUILayout.Button("Overwrite File"))
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
