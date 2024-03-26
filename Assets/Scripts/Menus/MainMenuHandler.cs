using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public void ClickPlayButton()
    {
        Debug.Log("play");
        
        // go back to game - close main menu 
        MainMenuController.Instance.CloseMainMenu();
    }
    
    public void ClickNewGameButton()
    {
        Debug.Log("new game");
        
        // TODO - reset saved game 
    }
    
    public void ClickSettingsButton()
    {
        Debug.Log("settings");
        
        // TODO - open the settings menu prefab 
    }
    
    public void ClickQuitButton()
    {
        Debug.Log("quit");
        
        // close the game 
        UnityEditor.EditorApplication.isPlaying = false; 
        Application.Quit();
    }
}
