using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropLoad : EditorWindow
{
    private string backdropPrefabPath = "Assets/Prefabs/TheatreBackdrop/Backdrop.prefab";
    private string layerPrefabPath = "Assets/Prefabs/TheatreBackdrop/Layer.prefab";
    private string spriteObjectPrefabPath = "Assets/Prefabs/TheatreBackdrop/SpriteObject.prefab";
    
    public static GameObject BackdropPrefab { get; private set; }
    public static GameObject LayerPrefab { get; private set; }
    public static GameObject SpriteObjectPrefab { get; private set; }
    
    public static List<GameObject> BackdropHolders { get; private set; }
    public static List<GameObject> Backdrops { get; private set; }
    public static List<GameObject> Layers { get; private set; }
    public static List<GameObject> Sprites { get; private set; }
    
    public void Load()
    {
        LoadStage();
        LoadPrefabs();
    }
    
    private void LoadStage()
    {
        // load backdrop holders 
        BackdropHolders = GameObject.FindGameObjectsWithTag("BackdropHolder").ToList();
        
        // load the backdrops 
        if (BackdropSelect.SelectedBackdropHolder != null)
        {
            Backdrops = GameObject.FindGameObjectsWithTag("Backdrop")
                        .Where(child => child.transform.IsChildOf(BackdropSelect.SelectedBackdropHolder.transform))
                        .ToList();
        }

        // load the layers of the backdrop 
        if (BackdropSelect.SelectedBackdrop != null)
        {
            if (BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>() != null)
            {
                Layers = BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().Layers; 
            }
        }
        
        // load the sprites of the layer 
        if (BackdropSelect.SelectedLayer != null)
        {
            if (BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>() != null)
            {
                Sprites = BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>().Sprites; 
            }
        }
    }

    private void LoadPrefabs()
    {
        BackdropPrefab = AssetDatabase.LoadAssetAtPath(backdropPrefabPath, typeof(GameObject)) as GameObject;
        LayerPrefab = AssetDatabase.LoadAssetAtPath(layerPrefabPath, typeof(GameObject)) as GameObject;
        SpriteObjectPrefab = AssetDatabase.LoadAssetAtPath(spriteObjectPrefabPath, typeof(GameObject)) as GameObject;
    }
}