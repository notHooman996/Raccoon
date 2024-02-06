using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackdropLayerEditor : EditorWindow
{
    private Vector2 scrollPosition;

    private string[] spritePaths;

    public void DrawEditor()
    {
        RefreshSpriteList();
        
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
            Sprite currentSelectedSprite = BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().SelectedSprite; 
            
            // display dropdown menu for selecting a sprite 
            int selectedIndex = EditorGUILayout.Popup("Select Sprite: ", GetSelectedIndex(currentSelectedSprite), GetSpriteNames());
            
            // set selected sprite 
            Sprite selectedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(spritePaths[selectedIndex]));
            if (selectedSprite != null)
            {
                if (currentSelectedSprite != selectedSprite)
                {
                    // apply selected sprite to the sprite object 
                    BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().SelectedSprite = selectedSprite; 
                    BackdropLoad.Sprites[i].GetComponent<BackdropSprite>().ApplySprite();
                    BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>().DrawSprites();
                }
            }
        }
        
        EditorGUILayout.EndVertical();
    }

    private int GetSelectedIndex(Sprite sprite)
    {
        for (int i = 0; i < spritePaths.Length; i++)
        {
            if (sprite == AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(spritePaths[i])))
            {
                return i; 
            }
        }

        return 0; 
    }

    private void RefreshSpriteList()
    {
        spritePaths = AssetDatabase.FindAssets("t:sprite", new[] { "Assets/Resources/Sprites" });
    }

    private string[] GetSpriteNames()
    {
        string[] spriteNames = new string[spritePaths.Length];

        for (int i = 0; i < spritePaths.Length; i++)
        {
            string path = spritePaths[i];
            string spriteName = System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(path));
            spriteNames[i] = spriteName;
        }
        
        return spriteNames; 
    }

    private void SpriteLocalTransform(int i)
    {
        EditorGUILayout.BeginVertical("box");

        // Note - to set the position, do it relative to the layers axis 
        Vector3 currentLocalPosition = BackdropLoad.Sprites[i].transform.localPosition; 
        Vector2 newLocalPosition = EditorGUILayout.Vector2Field("Sprite Local Position: ", new Vector2(currentLocalPosition.x, currentLocalPosition.z));
        Vector3 updatedLocalPosition = new Vector3(newLocalPosition.x, 0f, -newLocalPosition.y);
        BackdropLoad.Sprites[i].transform.localPosition = updatedLocalPosition; 
        
        EditorGUILayout.EndVertical();
    }
}