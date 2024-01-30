using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OldBackdropGizmos : MonoBehaviour
{
    public static OldBackdropGizmos Instance; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("BackdropFloor");
        GameObject[] layers = GameObject.FindGameObjectsWithTag("BackdropLayer");

        foreach (GameObject floor in floors)
        {
            Vector3[] corners = GetPlaneCorners(floor);

            if (corners != null)
            {
                DrawFloorOutline(corners[0], corners[1]);
                DrawFloorOutline(corners[1], corners[2]);
                DrawFloorOutline(corners[2], corners[3]);
                DrawFloorOutline(corners[3], corners[0]);
            }
        }
        
        foreach (GameObject layer in layers)
        {
            Vector3[] corners = GetPlaneCorners(layer);

            if (corners != null)
            {
                DrawLayerOutline(corners[0], corners[1]);
                DrawLayerOutline(corners[1], corners[2]);
                DrawLayerOutline(corners[2], corners[3]);
                DrawLayerOutline(corners[3], corners[0]);
            }
        }
    }

    private Vector3[] GetPlaneCorners(GameObject go)
    {
        // get mesh filter component 
        MeshFilter meshFilter = go.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            List<Vector3> vertices = meshFilter.sharedMesh.vertices.ToList(); 
            
            // sort by x value 
            vertices.Sort((v1, v2) => v1.x.CompareTo(v2.x));

            float minX = vertices.First().x;
            float maxX = vertices.Last().x;
            
            // sort by y value 
            vertices.Sort((v1, v2) => v1.y.CompareTo(v2.y));

            float minY = vertices.First().y;
            float maxY = vertices.Last().y;
            
            // sort by z value 
            vertices.Sort((v1, v2) => v1.z.CompareTo(v2.z));

            float minZ = vertices.First().z;
            float maxZ = vertices.Last().z;
            
            // get game objects transform 
            Transform goTransform = go.transform; 

            // calculate the corner positions 
            Vector3 bottomLeft = goTransform.TransformPoint(new Vector3(minX, minY, minZ));
            Vector3 bottomRight = goTransform.TransformPoint(new Vector3(maxX, minY, minZ));
            Vector3 topLeft = goTransform.TransformPoint(new Vector3(maxX, maxY, maxZ));
            Vector3 topRight = goTransform.TransformPoint(new Vector3(minX, maxY, maxZ));
            
            Vector3[] corners = new Vector3[4] { bottomLeft, bottomRight, topLeft, topRight };
            
            return corners; 
        }

        return null;
    }

    private void DrawFloorOutline(Vector3 from, Vector3 to)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(from, to);
    }

    private void DrawLayerOutline(Vector3 from, Vector3 to)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(from, to);
    }
    #endif
}
