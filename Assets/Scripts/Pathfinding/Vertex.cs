using System;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public string name;
    public List<Edge> edges;

    [NonSerialized] public float distance; 
    [NonSerialized] public float f; // distance to a node 
    [NonSerialized] public float g; // weight of a node 
    [NonSerialized] public float h; // total distance from start to the node 
    [NonSerialized] public Vertex predecessor;

    public float x;
    public float z;

    public Vertex(string name, float x, float z)
    {
        this.name = name;
        this.x = x;
        this.z = z;
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
