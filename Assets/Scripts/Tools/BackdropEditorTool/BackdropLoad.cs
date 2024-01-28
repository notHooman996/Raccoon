using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropLoad : EditorWindow
{
    private string backdropPrefabPath = "Assets/Prefabs/TheatreBackdrop/Backdrop.prefab";
    private string layerPrefabPath = "Assets/Prefabs/TheatreBackdrop/Layer.prefab";
    
    public static GameObject BackdropHolderObject { get; set; }
    public static GameObject BackdropPrefab { get; private set; }
    public static GameObject LayerPrefab { get; private set; }

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
        BackdropSelect.Backdrops = GameObject.FindGameObjectsWithTag("Backdrop").ToList();
    }

    private void LoadPrefabs()
    {
        BackdropPrefab = AssetDatabase.LoadAssetAtPath(backdropPrefabPath, typeof(GameObject)) as GameObject;
        LayerPrefab = AssetDatabase.LoadAssetAtPath(layerPrefabPath, typeof(GameObject)) as GameObject;
    }
}