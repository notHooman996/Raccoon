using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpriteEntry
{
    public string key;
    public Sprite value;
}

public class PlayerSprites : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public List<SpriteEntry> spriteList = new List<SpriteEntry>();
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private Vector3 cameraDirection;

    [Range(0f, 180f)][SerializeField] private float backAngle = 65f;
    [Range(0f, 180f)][SerializeField] private float sideAngle = 155f;

    private void Awake()
    {
        // Populate dictionary from the list
        foreach (var entry in spriteList)
        {
            if (!sprites.ContainsKey(entry.key))
            {
                sprites.Add(entry.key, entry.value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the spriterenderer 
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites["Front"]; 
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Billboard();
        
        ChangeSprite();
    }
    
    private void Billboard()
    {
        cameraDirection = CameraHandler.Instance.GetCurrentCamera().transform.forward;
        cameraDirection.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }

    private void ChangeSprite()
    {
        Vector3 camForwardVector = new Vector3(CameraHandler.Instance.GetCurrentCamera().transform.forward.x, 0f, CameraHandler.Instance.GetCurrentCamera().transform.forward.z);
    
        float signedAngle = Vector3.SignedAngle(transform.parent.forward, camForwardVector, Vector3.up);
    
        Vector2 spriteDirection = new Vector2(0f, -1f);
    
        float angle = Mathf.Abs(signedAngle);
    
        if (angle < backAngle)
        {
            // back sprite 
            spriteDirection = new Vector2(0f, -1f);
            spriteRenderer.sprite = sprites["Back"];
        }
        else if (angle < sideAngle)
        {
            // side sprite 
            if (signedAngle < 0)
            {
                spriteDirection = new Vector2(-1f, 0f);
                spriteRenderer.sprite = sprites["Right"];
            }
            else
            {
                spriteDirection = new Vector2(1f, 0f);
                spriteRenderer.sprite = sprites["Left"]; 
            }
        }
        else
        {
            // front sprite 
            spriteDirection = new Vector2(0f, 1f);
            spriteRenderer.sprite = sprites["Front"]; 
        }
    }
}
