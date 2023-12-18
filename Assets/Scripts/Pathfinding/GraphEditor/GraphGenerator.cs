using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator
{
    private static List<GameObject> walkableObjects;
    private static List<GameObject> colliderObjects;

    private static List<Vertex> vertices;

    private static float playerSize = 3f; // TODO - move somewhere else, could be used as variable interactDistance as well 

    public static List<Vertex> GetVertices()
    {
        return vertices; 
    }
    
    public static void Generate(List<GameObject> walkables, List<GameObject> colliders)
    {
        vertices = new List<Vertex>();
        
        walkableObjects = walkables;
        colliderObjects = colliders;

        GenerateVertices();
    }
    
    private static void GenerateVertices()
    {
        foreach (GameObject colliderObject in colliderObjects)
        {
            // get the meshe of the collider object 
            Mesh mesh = colliderObject.GetComponent<Mesh>();
            
            // get the corners of the mesh 
            
        }
        
        
        
        
        foreach (GameObject walkable in walkableObjects)
        {
            // get the position 
            Vector3 objectPosition = walkable.transform.position; 

            // get the scale in each direction 
            Vector3 objectScale = walkable.transform.localScale; 

            // add vertex 
            vertices.Add(new Vertex("A", objectPosition));
        }
    }
}
