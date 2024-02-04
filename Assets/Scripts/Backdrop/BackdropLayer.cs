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
        // set sprite objects position relative to layer 
        spriteObject.transform.localPosition = new Vector3(spriteObject.transform.localPosition.x, spriteObject.transform.localPosition.y, 0f);
        // set sprite objects rotation to match the layers rotation 
        spriteObject.transform.localRotation = transform.localRotation; 
        
        Sprites.Add(spriteObject);
    }

    public void DrawSprites()
    {
        for (int i = 0; i < Sprites.Count; i++)
        {
            // set sprite objects position relative to layer 
            Sprites[i].transform.localPosition = new Vector3(Sprites[i].transform.localPosition.x, Sprites[i].transform.localPosition.y, 0f);
            // set sprite objects rotation to match the layers rotation 
            Sprites[i].transform.localRotation = transform.localRotation; 
        }
    }
}