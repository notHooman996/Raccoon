using UnityEngine;

public class Path
{
    public AStar aStar;
    public Vector3 from;
    public Vector3 to;

    public Path(Graph graph, Vector3 from, Vector3 to)
    {
        this.from = from;
        this.to = to;
        aStar = new AStar(graph);
    }
}
