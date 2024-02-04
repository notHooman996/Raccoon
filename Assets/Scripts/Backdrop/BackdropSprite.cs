using UnityEngine;

public class BackdropSprite : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite;
    
    public Sprite SelectedSprite
    {
        get { return selectedSprite; }
        set { selectedSprite = value; }
    }
    
    // method is called when script is loaded 
    private void OnValidate()
    {
        ApplySprite();
    }

    public void ApplySprite()
    {
        // get gameobjects sprite renderer 
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // set sprite on sprite renderer 
        spriteRenderer.sprite = selectedSprite; 
    }
}