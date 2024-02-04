using UnityEditor;
using UnityEngine;

public class BackdropLayerEditor : EditorWindow
{
    private Vector2 scrollPosition;

    private string[] spriteOptions; 
    
    public void DrawEditor()
    {
        // load the sprite options 
        spriteOptions = GetSpriteNames();
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        if (BackdropSelect.SelectedLayer != null && BackdropLoad.Sprites != null)
        {
            DrawList();
        }
        else
        {
            EditorGUILayout.LabelField("Select a layer to edit.");
        }
        
        EditorGUILayout.EndScrollView();
    }

    private void DrawList()
    {
        EditorGUILayout.LabelField("Layer sprites: ");
        
        if (BackdropLoad.Sprites.Count > 0)
        {
            for (int i = 0; i < BackdropLoad.Sprites.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                SpriteElement(i);
                
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Add sprites to the layer to edit.");
        }
    }

    private void SpriteElement(int i)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Name: " + BackdropLoad.Sprites[i].name);
        
        EditorGUILayout.LabelField("rotation: " + BackdropLoad.Sprites[i].transform.localRotation.eulerAngles);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        SpriteSelect(i);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        SpriteLocalTransform(i);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void SpriteSelect(int i)
    {
        EditorGUILayout.BeginVertical("box");

        if (BackdropLoad.Sprites[i].GetComponent<BackdropSprite>() != null)
        {
            // get selected sprite 
            Sprite selectedSprite = BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().SelectedSprite; 
            
            // display dropdown menu for selecting a sprite 
            int selectedIndex = EditorGUILayout.Popup("Select Sprite: ", GetSelectedIndex(selectedSprite), spriteOptions);
            
            // set selected sprite 
            selectedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spriteOptions[selectedIndex]); 
            
            // apply selected sprite to the sprite object 
            BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().SelectedSprite = selectedSprite; 
            BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().ApplySprite();
            BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>().DrawSprites();
        }
        
        EditorGUILayout.EndVertical();
    }

    private int GetSelectedIndex(Sprite sprite)
    {
        for (int i = 0; i < spriteOptions.Length; i++)
        {
            if (sprite == AssetDatabase.LoadAssetAtPath<Sprite>(spriteOptions[i]))
            {
                return i; 
            }
        }

        return 0; 
    }

    private string[] GetSpriteNames()
    {
        string[] spritePaths = AssetDatabase.FindAssets("t:sprite", new[] { "Assets/Resources/Sprites" }); 
        string[] spriteNames = new string[spritePaths.Length];

        for (int i = 0; i < spritePaths.Length; i++)
        {
            spriteNames[i] = AssetDatabase.GUIDToAssetPath(spritePaths[i]);
        }

        return spriteNames; 
    }

    private void SpriteLocalTransform(int i)
    {
        EditorGUILayout.BeginVertical("box");

        BackdropLoad.Sprites[i].transform.localPosition = EditorGUILayout.Vector2Field("Sprite Local Position: ", BackdropLoad.Sprites[i].transform.localPosition);
        
        EditorGUILayout.EndVertical();
    }
}