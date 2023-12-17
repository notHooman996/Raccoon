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

    public void Add(string name, float x, float y, float z)
    {
        vertices.Add(new Vertex(name, x, y, z));
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
