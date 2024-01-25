using System.Collections.Generic;
using UnityEditor;
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

        //GenerateVertices();
    }
    
    private static void GenerateVertices()
    {
        foreach (GameObject colliderObject in colliderObjects)
        {
            // get the mesh filter component from the object 
            MeshFilter meshFilter = colliderObject.GetComponent<MeshFilter>();

            if (meshFilter != null)
            {
                // get the mesh of the collider object 
                Mesh mesh = meshFilter.mesh; 

                // get the corners of the mesh 
                if (mesh != null)
                {
                    // get the mesh vertices 
                    Vector3[] meshVertices = mesh.vertices;

                    // get the transom of the object 
                    Transform objTransform = colliderObject.transform;

                    // loop through each mesh vertex to get the worldspace position 
                    for (int i = 0; i < meshVertices.Length; i++)
                    {
                        // transform of the mesh vertex from local space to world space 
                        Vector3 cornerWorldPosition = objTransform.TransformPoint(meshVertices[i]);

                        // check where mesh vertex intersects with walkables 
                        foreach (GameObject walkableObject in walkableObjects)
                        {
                            // get the walkable object as a plane 
                            Plane plane = new Plane(walkableObject.transform.up, walkableObject.transform.position);
                            
                            // create ray from the mesh vertex along the normal towards the walkable 
                            Ray ray = new Ray(cornerWorldPosition, -plane.normal);

                            float enter; 
                            // check for intersection between ray and the walkable 
                            if (plane.Raycast(ray, out enter))
                            {
                                // get the intersection point 
                                Vector3 intersectionPoint = ray.GetPoint(enter);
                                
                                // Output the intersection point
                                Debug.Log("Intersection point for corner " + i + ": " + intersectionPoint);
                                vertices.Add(new Vertex("A", intersectionPoint));
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("Mesh not found.");
                }
            }
        }
    }

    public static void CreateVertices()
    {
        Debug.Log("test1");
        // check for mouse left click 
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0) 
        {
            Debug.Log("test2");
            
            // cast a ray from the mouse position in relation to the camera's view 
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // check if ray hits objects in walkable list 
                foreach (GameObject gameObject in walkableObjects)
                {
                    if (hitInfo.collider.gameObject == gameObject)
                    {
                        Debug.Log("Hit object: " + gameObject.name);
                        
                        // create vertex at entry point 
                    }
                }
            }
        }
    }

    private static void CreateVertex()
    {
        
    }
}
