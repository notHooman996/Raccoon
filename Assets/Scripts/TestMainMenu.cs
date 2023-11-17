using Cinemachine;
using UnityEngine;
using System.Collections;

public class TestMainMenu : MonoBehaviour
{
    public GameObject mainmenuPrefab; // reference to the canvas ui prefab 
    private CanvasGroup canvasGroup;
    private GameObject canvasInstance; // reference to the instantiated prefab 
    private bool isMenu = false;

    private CinemachineBrain cinemachineBrain;
    
    private void Start()
    {
        mainmenuPrefab.SetActive(true);
    }

    private void Update()
    {
        if (InputHandler.Instance.GetPauseInput()) // TODO - temporary using pause for testing the main menu 
        {
            isMenu = !isMenu;

            if (isMenu)
            {
                OpenMainMenu();
            }
            else
            {
                CloseMainMenu();
            }
        }
    }

    /// <summary>
    /// Opens the main menu prefab 
    /// </summary>
    public void OpenMainMenu()
    {
        // move camera to sky - change camera with the CameraController 
        CameraHandler.Instance.SwitchCamera(CameraHandler.Instance.GetCameraByName("SkyCamera"));
        
        // when camera transition is complete - open the menu 
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            StartCoroutine(CameraTransition());
        }
    }
    
    /// <summary>
    /// Closes the main menu prefab 
    /// </summary>
    public void CloseMainMenu()
    {
        // fade the canvas before continuing 
        if (canvasInstance != null)
        {
            canvasGroup = canvasInstance.GetComponent<CanvasGroup>();
            StartCoroutine(FadeCanvasGroup(canvasGroup.alpha, 0f, 2f));
        }

        // once fading is over - change camera and remove prefab 
        StartCoroutine(FadingOver());
    }

    /// <summary>
    /// Coroutine
    /// Waits till camera is is the sky.
    /// Sets main menu prefab, that then fades in. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CameraTransition()
    {
        // wait till camera has fully transitioned to the sky 
        yield return new WaitUntil(() => 
            (CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera == CameraHandler.Instance.GetCameraByName("SkyCamera") 
            && !cinemachineBrain.IsBlending
            );
        
        AddPrefabToScene();
        
        // fade the canvas in 
        if (canvasInstance != null)
        {
            canvasGroup = canvasInstance.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeCanvasGroup(canvasGroup.alpha, 1f, 2f));
        }
    }
    
    /// <summary>
    /// Coroutine
    /// Waits till canvas has faded out and become invisible.
    /// Removes main menu prefab, then changes camera to the previous camera. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadingOver()
    {
        // wait till the canvas is invisible - then remove prefab and change camera 
        yield return new WaitUntil(() => canvasGroup.alpha == 0f);
        RemovePrefabFromScene();
        CameraHandler.Instance.SwitchToPrevCamera();
    }

    /// <summary>
    /// Coroutine
    /// Changes the alpha of the canvas group over time.
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="targetAlpha"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha, float duration)
    {
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            canvasGroup.alpha = alpha;

            yield return null; 
        }
    }
    
    /// <summary>
    /// Adds the main menu prefab to the scene. 
    /// </summary>
    public void AddPrefabToScene()
    {
        canvasInstance = Instantiate(mainmenuPrefab);
    }

    /// <summary>
    /// Gets the instantiated prefab 
    /// </summary>
    /// <returns></returns>
    private GameObject GetInstantiatedPrefab()
    {
        return canvasInstance;
    }

    /// <summary>
    /// Removes the instantiated main menu prefab from the scene. 
    /// </summary>
    public void RemovePrefabFromScene()
    {
        if (GetInstantiatedPrefab() != null)
        {
            Destroy(GetInstantiatedPrefab());
        }
    }
}
