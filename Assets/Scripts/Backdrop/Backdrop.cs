using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    public List<GameObject> Layers { get; private set; }

    // method is called when script is loaded 
    private void OnValidate()
    {
        LoadLayers();
        DrawLayers();
    }

    private void LoadLayers()
    {
        Layers = transform
                .GetComponentsInChildren<Transform>(true)
                .Where(child => child.CompareTag("BackdropLayer"))
                .Select(child => child.gameObject)
                .ToList();
    }
    
    public void AddLayer(GameObject layer)
    {
        // set layer rotation depending to backdrop rotation 
        Quaternion initialLayerRotation = layer.transform.localRotation;
        Quaternion newLayerRotation = transform.localRotation * initialLayerRotation; 
        layer.transform.localRotation = newLayerRotation; 
        
        Layers.Add(layer);

        DrawLayers();
    }
    
    // create method that positions the layers correctly in relation to the backdrop 
    public void DrawLayers()
    {
        AdjustLayerIds();
        
        // arrange the layers along the local z-axis with spacing 
        float localOffsetZ = 0f; // initial offset 
        for (int i = 0; i < Layers.Count; i++)
        {
            // update layer position 
            localOffsetZ += i > 0 ? Layers[i - 1].GetComponent<BackdropLayer>().LayerSpacing : 0f; // add the previous layers spacing 
            Vector3 newPosition = new Vector3(0f, Layers[i].transform.localPosition.y, localOffsetZ);
            Layers[i].transform.localPosition = newPosition;  
        }
    }

    public int GetHighestLayerId()
    {
        AdjustLayerIds();
        
        int highestID = 0;

        foreach (GameObject layer in Layers)
        {
            int id = layer.GetComponent<BackdropLayer>().LayerID;

            if (id > highestID)
            {
                highestID = id; 
            }
        }

        return highestID; 
    }

    private void AdjustLayerIds()
    {
        // sort the layers based on id 
        Layers.Sort((a, b) => a.GetComponent<BackdropLayer>().LayerID.CompareTo(b.GetComponent<BackdropLayer>().LayerID));
        
        for (int i = 0; i < Layers.Count; i++)
        {
            Layers[i].GetComponent<BackdropLayer>().LayerID = i;
        }
    }
}
