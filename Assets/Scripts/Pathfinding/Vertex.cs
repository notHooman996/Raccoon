using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vertex
{
    public string name;
    [NonSerialized] public List<Edge> edges;

    [NonSerialized] public float distance; 
    [NonSerialized] public float f; // distance to a node 
    [NonSerialized] public float g; // weight of a node 
    [NonSerialized] public float h; // total distance from start to the node 
    [NonSerialized] public Vertex predecessor;

    public Vector3 position; 
    
    [NonSerialized]public float x;
    [NonSerialized]public float y;
    [NonSerialized]public float z;

    public Vertex(string name, Vector3 position)
    {
        this.name = name;
        this.position = position; 
        edges = new List<Edge>();
    }

    public float CalculateF()
    {
        return g + h; 
    }

    public List<Vertex> GetNeighbours()
    {
        List<Vertex> neighbours = new List<Vertex>();

        foreach (Edge edge in edges)
        {
            neighbours.Add(edge.GetToVertex());
        }

        return neighbours; 
    }
}
