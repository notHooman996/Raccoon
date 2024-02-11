using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropLoad : EditorWindow
{
    private string backdropPrefabPath = "Assets/Prefabs/TheatreBackdrop/Backdrop.prefab";
    private string layerPrefabPath = "Assets/Prefabs/TheatreBackdrop/Layer.prefab";
    private string spriteObjectPrefabPath = "Assets/Prefabs/TheatreBackdrop/SpriteObject.prefab";
    
    public static GameObject BackdropHolderObject { get; set; }
    public static GameObject BackdropPrefab { get; private set; }
    public static GameObject LayerPrefab { get; private set; }
    public static GameObject SpriteObjectPrefab { get; private set; }
    
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
        // load backdrop holder 
        BackdropHolderObject = GameObject.FindGameObjectWithTag("BackdropHolder");
        
        // load the backdrops 
        Backdrops = GameObject.FindGameObjectsWithTag("Backdrop").ToList();
        
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