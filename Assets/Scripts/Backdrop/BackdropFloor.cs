using UnityEngine;

public class BackdropFloor : MonoBehaviour
{
    public float FloorWidth { get; private set; }
    
    // TODO - should also have list of sprites 

    public void CalculateFloorWidth()
    {
        // reset width 
        FloorWidth = 0; 
        
        // get the backdrop parent opbject 
        Transform backdropTransform = transform.parent;

        if (backdropTransform != null)
        {
            // loop through all child transforms of the backdrop 
            for (int i = 0; i < backdropTransform.childCount; i++)
            {
                // get the children that are layers 
                Transform layerTransform = backdropTransform.Find("BackdropLayer");

                if (layerTransform != null)
                {
                    // access the layer gameobject 
                    GameObject layer = layerTransform.gameObject; 
                    
                    // get the data component 
                    BackdropLayer layerData = layer.GetComponent<BackdropLayer>();

                    if (layerData != null)
                    {
                        // calculate the collective width of all layers 
                        FloorWidth += layerData.LayerSpacing;
                    }
                }
            }
        }
    }
}