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
        foreach (Vertex vertex in GraphGenerator.GetVertices())
        {
            DrawVertex(vertex.position);
        }
        
        // DrawVertex(Vector3.zero);
        // DrawEdge(Vector3.zero, new Vector3(1, 0, 1));
    }

    public void DrawVertex(Vector3 position)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(position, 0.1f);
    }

    private void DrawEdge(Vector3 from, Vector3 to)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(from, to);
    }
    #endif
}
