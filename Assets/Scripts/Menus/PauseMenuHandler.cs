using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public void ClickResumeButton()
    {
        Debug.Log("resume");
        
        // close the pause menu prefab 
        PauseMenuController.Instance.ClosePauseMenu();
    }
    
    public void ClickSettingsButton()
    {
        Debug.Log("settings");
        
        // TODO - open the settings menu prefab 
    }
    
    public void ClickMainMenuButton()
    {
        Debug.Log("main menu");
        
        // close the pause menu prefab 
        PauseMenuController.Instance.ClosePauseMenu();
        
        // open the main menu prefab 
        MainMenuController.Instance.OpenMainMenu();
    }
}
