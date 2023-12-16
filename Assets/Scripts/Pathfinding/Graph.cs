using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Vertex> vertices;

    public void Remove(string name)
    {
        foreach (Vertex vertex in vertices)
        {
            if (vertex.name == name)
            {
                vertices.Remove(vertex);
            }
        }
    }

    public void Add(string name, float x, float z)
    {
        vertices.Add(new Vertex(name, x, z));
    }

    public void Clear()
    {
        vertices.Clear();
    }

    public void CalculateEdges()
    {
        if (vertices != null)
        {
            foreach (Vertex vertex1 in vertices)
            {
                vertex1.edges = new List<Edge>();

                foreach (Vertex vertex2 in vertices)
                {
                    if (!vertex1.Equals(vertex2))
                    {
                        
                    }
                }
            }
        }
    }
}
