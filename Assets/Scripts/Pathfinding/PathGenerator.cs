using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    // singleton 
    public static PathGenerator Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    private string folderPath = "Assets/DataFiles/";
    private string selectedJSONFile = "Graph_RaccoonHome.json"; // TODO - load from somewhere 

    public GameObject player;
    public GameObject target;

    AStar aStar;
    private Graph graph;
    private Vertex start;
    private Vertex end;

    public List<Vertex> path = new List<Vertex>();

    private void Start()
    {
        // TODO - make sure to load new file whenever changing stage 
        string jsonContent = LoadFile();
        graph = ConvertToJsonObject<Graph>(jsonContent);
    }

    private void Update()
    {
        if (InputHandler.Instance.GetIsMouseInput())
        {
            CreateGraph();
        }
    }

    public void CreateGraph()
    {
        start = new Vertex("Start", player.transform.position);
        end = new Vertex("End", AttributesPointAndClick.Instance.GoalPosition);
        graph.vertices.Add(start);
        graph.vertices.Add(end);
        graph = CalculateGraphConnections(graph);

        aStar = new AStar(graph);
        aStar.Run(start, end);

        path = aStar.pathResult; 

        graph.vertices.Remove(start);
        graph.vertices.Remove(end);
    }

    private Graph CalculateGraphConnections(Graph graph)
    {
        foreach (Vertex vertex1 in graph.vertices)
        {
            vertex1.edges = new List<Edge>();

            foreach (Vertex vertex2 in graph.vertices)
            {
                // Skip adding edges for the same vertex
                if (!vertex1.Equals(vertex2))
                {
                    Vector3 position1 = new Vector3(vertex1.position.x, 0, vertex1.position.z);
                    Vector3 position2 = new Vector3(vertex2.position.x, 0, vertex2.position.z);

                    // Cast a ray between points.
                    bool isHit = Physics.Linecast(position1, position2, out RaycastHit hit);

                    // If it does not hit something and the collider is tagged as "Obstacle" or "Interactable", then add edge 
                    if (!isHit || (!hit.collider.CompareTag("Obstacle") && !hit.collider.CompareTag("Interactable")))
                    {
                        Edge edge = new Edge(vertex1, vertex2);
                        vertex1.edges.Add(edge);
                    }
                }
            }
        }

        return graph;
    }

    private void OnDrawGizmos()
    {
        for (int i=0; i<aStar?.pathResult?.Count; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(aStar.pathResult[i].position, 0.5f);
            if (i!=aStar.pathResult.Count-1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(aStar.pathResult[i].position, aStar.pathResult[i+1].position);
            }
        }
    }

    private T ConvertToJsonObject<T>(string jsonContent)
    {
        return JsonUtility.FromJson<T>(jsonContent);
    }

    private string LoadFile()
    {
        // read the entire file as a string
        string filePath = System.IO.Path.Combine(folderPath, selectedJSONFile);
        return File.ReadAllText(filePath);
    }
}
