using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GraphGenerator
{
    private float proximityRadius = 3; // TODO - player gameobject size radius 
    
    public void CreateVertex(SceneView sceneView, List<Vertex> vertices)
    {
        // check for mouse left click 
        Event currentEvent = Event.current; 
        if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown && currentEvent.button == 0) 
        {
            // cast a ray from the mouse position in relation to the camera's view 
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // check if ray hits objects in walkable list 
                foreach (GameObject gameObject in GraphCreator.GetWalkableList())
                {
                    if (hitInfo.collider.gameObject == gameObject)
                    {
                        // create vertex at entry point 
                        vertices.Add(new Vertex("test", hitInfo.point));
                    }
                }
            }
        }
    }
    
    public List<Vertex> CheckVertices()
    {
        List<Vertex> wrongVertices = new List<Vertex>();
        
        foreach (Vertex vertex in GraphCreator.GetVertices())
        {
            bool isVertexTooClose = false; 
            foreach (GameObject colliderObject in GraphCreator.GetColliderList())
            {
                float distance = Vector3.Distance(colliderObject.transform.position, vertex.position);
                if (distance <= proximityRadius) // TODO - calculate common radius for player and collider object 
                {
                    isVertexTooClose = true;
                    break; 
                }
            }

            if (isVertexTooClose)
            {
                wrongVertices.Add(vertex);
            }
        }

        return wrongVertices;
    }
    
    public List<Edge> GenerateEdges()
    {
        List<Edge> edges = new List<Edge>();
        
        // connect all vertices with edges 
        foreach (Vertex vertex1 in GraphCreator.GetVertices())
        {
            foreach (Vertex vertex2 in GraphCreator.GetVertices())
            {
                // make sure not to make edges at same vertex 
                if (vertex1 != vertex2)
                {
                    bool hitAnyObject = false; 
                    
                    // define raycast start and end point
                    Vector3 startPoint = vertex1.position;
                    Vector3 endPoint = vertex2.position;

                    Ray ray = new Ray(startPoint, endPoint - startPoint);
                    RaycastHit hit; 
                    
                    // check if collider in the way 
                    foreach (GameObject colliderObject in GraphCreator.GetColliderList())
                    {
                        if (colliderObject != null)
                        {
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.collider.gameObject == colliderObject)
                                {
                                    hitAnyObject = true;
                                    break; // exit the loop 
                                }
                            }
                        }
                    }

                    // if no collision object was hit, then create edge
                    if (!hitAnyObject)
                    {
                        edges.Add(new Edge(vertex1, vertex2));
                    }
                }
            }
        }

        return edges; 
    }
}
