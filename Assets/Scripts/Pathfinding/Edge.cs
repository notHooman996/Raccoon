using UnityEngine;

public class Edge
{
    public Vertex from;
    public Vertex to;

    public Edge(Vertex from, Vertex to)
    {
        this.from = from;
        this.to = to; 
    }

    public Vertex GetFromVertex()
    {
        return from; 
    }

    public Vertex GetToVertex()
    {
        return to; 
    }
}
