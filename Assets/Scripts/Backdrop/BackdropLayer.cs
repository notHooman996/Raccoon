using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackdropLayer : MonoBehaviour
{
    [SerializeField] private int layerID;
    [SerializeField] private float layerSpacing;
    
    public int LayerID
    {
        get { return layerID; }
        set { layerID = value; }
    }
    
    public float LayerSpacing
    {
        get { return layerSpacing; }
        set { layerSpacing = value; }
    }
    
    public List<GameObject> Sprites { get; private set; }
    
    // method is called when script is loaded 
    private void OnValidate()
    {
        LoadSprites();
        DrawSprites();
    }
    
    private void LoadSprites()
    {
        Sprites = transform
            .GetComponentsInChildren<Transform>(true)
            .Where(child => child.CompareTag("Sprite"))
            .Select(child => child.gameObject)
            .ToList();
    }

    public void AddSprite(GameObject spriteObject)
    {
        // set the local position and rotation of the new sprite object in relation to the layer 
        spriteObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        spriteObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f); 
        
        Sprites.Add(spriteObject);

        DrawSprites();
    }

    public void DrawSprites()
    {
        
    }
}