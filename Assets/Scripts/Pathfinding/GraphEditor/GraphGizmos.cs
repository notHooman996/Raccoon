using System;
using UnityEditor;
using UnityEngine;

public class GraphGizmos : MonoBehaviour
{
    public static GraphGizmos Instance; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (Vertex vertex in GraphCreator.GetVertices())
        {
            if (GraphCreator.GetWrongVertices() == null || GraphCreator.GetWrongVertices().Count == 0)
            {
                DrawVertex(vertex.position);
            }
            
            foreach (Vertex wrongVertex in GraphCreator.GetWrongVertices())
            {
                if (vertex != wrongVertex)
                {
                    DrawVertex(vertex.position);
                }
            }
        }

        foreach (Vertex wrongVertex in GraphCreator.GetWrongVertices())
        {
            DrawWrongVertex(wrongVertex.position);
        }

        if (GraphCreator.GetChosenVertex() != null)
        {
            DrawChosenVertex(GraphCreator.GetChosenVertex().position);
        }

        foreach (Edge edge in GraphCreator.GetEdges())
        {
            DrawEdge(edge.from.position, edge.to.position);
        }
    }

    public void DrawVertex(Vector3 position)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(position, 0.1f);
    }
    
    public void DrawChosenVertex(Vector3 position)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(position, 0.15f);
    }

    private void DrawWrongVertex(Vector3 position)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.1f);
    }

    private void DrawEdge(Vector3 from, Vector3 to)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(from, to);
    }
    #endif
}
