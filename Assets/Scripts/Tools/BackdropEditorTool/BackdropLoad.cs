using System.Linq;
using UnityEditor;
using UnityEngine;

public class BackdropLoad : EditorWindow
{
    public static GameObject BackdropHolderObject { get; set; }
    
    public void LoadStage()
    {
        // load backdrop holder 
        BackdropHolderObject = GameObject.FindGameObjectWithTag("BackdropHolder");
        
        // load the backdrops 
        BackdropSelect.Backdrops = GameObject.FindGameObjectsWithTag("Backdrop").ToList();
    }
}