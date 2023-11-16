using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public void ClickPlayButton()
    {
        Debug.Log("play");
    }
    
    public void ClickNewGameButton()
    {
        Debug.Log("new game");
    }
    
    public void ClickSettingsButton()
    {
        Debug.Log("settings");
    }
    
    public void ClickQuitButton()
    {
        Debug.Log("quit");
    }
}
