using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite sprite; 
    
    // Start is called before the first frame update
    void Start()
    {
        // get the spriterenderer 
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite; 
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
