using UnityEditor;
using UnityEngine;

public class BackdropCreater : EditorWindow
{
    public void DrawCreater()
    {
        GUILayout.BeginHorizontal();
        AddBackdrop();
        AddLayer();
        AddSprite();
        GUILayout.EndHorizontal();
    }

    private void AddBackdrop()
    {
        if (GUILayout.Button("Add Backdrop"))
        {
            if (BackdropLoad.BackdropHolderObject == null)
            {
                // new backdrop holder object 
                BackdropLoad.BackdropHolderObject = new GameObject();
                BackdropLoad.BackdropHolderObject.name = "BackdropHolder";
                BackdropLoad.BackdropHolderObject.tag = "BackdropHolder";
            }
            
            // new backdrop gameobject 
            GameObject newBackdrop = Instantiate(BackdropLoad.BackdropPrefab);
            newBackdrop.name = "Backdrop"+BackdropLoad.Backdrops.Count; 
            newBackdrop.transform.parent = BackdropLoad.BackdropHolderObject.transform;
            newBackdrop.AddComponent<Backdrop>();
            BackdropLoad.Backdrops.Add(newBackdrop);
        }
    }

    private void AddLayer()
    {
        bool isClickable = BackdropSelect.SelectedBackdrop != null; 
        
        Color buttonColor = isClickable ? BackdropToolUtilities.DefaultColor : BackdropToolUtilities.UnavailableColor;
        GUI.backgroundColor = buttonColor; 
        
        if (GUILayout.Button("Add Layer"))
        {
            if (isClickable)
            {
                // new layer gameobject 
                GameObject newLayer = Instantiate(BackdropLoad.LayerPrefab);
                newLayer.name = "Layer" + BackdropLoad.Layers.Count; 
                newLayer.transform.parent = BackdropSelect.SelectedBackdrop.transform;
                // add component 
                newLayer.AddComponent<BackdropLayer>();
                // figure out the id 
                newLayer.GetComponent<BackdropLayer>().LayerID = BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().GetHighestLayerId() + 1;
                // set initial spacing 
                newLayer.GetComponent<BackdropLayer>().LayerSpacing = 0.5f;
                // add new layer to backdrop list 
                BackdropSelect.SelectedBackdrop.GetComponent<Backdrop>().AddLayer(newLayer);
            }
        }

        // reset color to default 
        GUI.backgroundColor = BackdropToolUtilities.DefaultColor; 
    }

    private void AddSprite()
    {
        bool isClickable = BackdropSelect.SelectedLayer != null; 
        
        Color buttonColor = isClickable ? BackdropToolUtilities.DefaultColor : BackdropToolUtilities.UnavailableColor;
        GUI.backgroundColor = buttonColor;

        if (GUILayout.Button("Add Sprite"))
        {
            if (isClickable)
            {
                // new sprite gameobject 
                // create new empty gameobject 
                GameObject newGameObject = new GameObject("Sprite" + BackdropLoad.Sprites.Count);
                // set parent 
                newGameObject.transform.parent = BackdropSelect.SelectedLayer.transform;
                // set tag 
                newGameObject.tag = "Sprite";
                // add script component 
                newGameObject.AddComponent<BackdropSprite>(); 
                // add a spriterenderer component to the new gameobject 
                newGameObject.AddComponent<SpriteRenderer>();
                // apply a default sprite 
                newGameObject.GetComponent<BackdropSprite>().SelectedSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Resources/Sprites/TestSprite.png"); // TODO - make a default 
                newGameObject.GetComponent<BackdropSprite>().ApplySprite();
                // add new sprite object to the list 
                BackdropSelect.SelectedLayer.GetComponent<BackdropLayer>().AddSprite(newGameObject);
            }
        }
        
        // reset color to default 
        GUI.backgroundColor = BackdropToolUtilities.DefaultColor; 
    }
}