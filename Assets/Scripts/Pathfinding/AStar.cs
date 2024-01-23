using System;
using System.Collections.Generic;

public class AStar
{
    public Graph graph;
    private float infinity = Int32.MaxValue;
    private bool pathable;
    public List<Vertex> pathResult;
    public float endDistance;

    public AStar(Graph graph)
    {
        this.graph = graph; 
    }

    public void Run(Vertex start, Vertex end)
    {
        string pathString = "";
        pathable = Calculate(start, end);

        if (pathable)
        {
            Vertex currentVertex = end;
            List<Vertex> path = new List<Vertex>();
            path.Add(end);

            while (currentVertex != start && currentVertex.predecessor != null)
            {
                currentVertex = currentVertex.predecessor; 
                path.Add(currentVertex);
            }

            path.Reverse();
            
            pathResult = path;

            foreach (Vertex vertex in pathResult)
            {
                pathString += vertex.name; 
            }
        }
    }

    public bool Calculate(Vertex start, Vertex end)
    {
        if (start == null || end == null)
        {
            return false; 
        }

        SortedSet<Vertex> openList = new SortedSet<Vertex>(new FCompare()); 
        List<Vertex> closedList = new List<Vertex>(); 

        openList.Add(start);
        Vertex current;
        List<Vertex> currentNeighbours;

        foreach (Vertex vertex in graph.vertices)
        {
            vertex.g = infinity;
            vertex.h = Euclidean(vertex, end);
        }

        start.g = 0;
        start.CalculateF();

        int i = 0;
        while (openList.Count > 0 && i < graph.vertices.Count)
        {
            i += 1;
            current = openList.Min;
            currentNeighbours = current.GetNeighbours();
            openList.Remove(current);

            if (current.name.Equals(end.name))
            {
                endDistance = end.f;
                return true; 
            }
            closedList.Add(current);

            foreach (Vertex nextVertex in currentNeighbours)
            {
                float newG = current.g + Euclidean(current, nextVertex);
                if (newG < nextVertex.g)
                {
                    nextVertex.predecessor = current;
                    nextVertex.g = newG;
                    nextVertex.f = nextVertex.CalculateF();

                    if (!closedList.Contains(nextVertex) && !openList.Contains(nextVertex))
                    {
                        openList.Add(nextVertex);
                    }

                    if (openList.Contains(nextVertex))
                    {
                        openList.Remove(nextVertex);
                        openList.Add(nextVertex);
                    }
                }
            }
        }

        return false; 
    }

    public float Euclidean(Vertex from, Vertex to)
    {
        float x = Math.Abs(to.x - from.x);
        float z = Math.Abs(to.z - from.z);
        float distance = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(z, 2));

        return distance; 
    }
}
