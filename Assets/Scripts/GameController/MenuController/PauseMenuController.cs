using UnityEngine;
using System.Collections;
using UnityEditor;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance; 
    
    private GameObject pausemenuPrefab; // reference to the canvas ui prefab 
    private CanvasGroup canvasGroup;
    private GameObject canvasInstance; // reference to the instantiated prefab 
    private bool isPaused = false; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // load the prefab 
        pausemenuPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Menus/PauseMenu.prefab");
        
        pausemenuPrefab.SetActive(true);
    }

    private void Update()
    {
        if (InputHandler.Instance.GetPauseInput())
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    /// <summary>
    /// Opens the pause menu prefab 
    /// </summary>
    public void OpenPauseMenu()
    {
        AttributesPlayer.Instance.CanPlayerMove = false; // TODO - change
        AddPrefabToScene();
        canvasGroup = canvasInstance.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup(0f, 1f, 2f));
    }

    /// <summary>
    /// Closes the pause menu prefab 
    /// </summary>
    public void ClosePauseMenu()
    {
        isPaused = false; 
        if (canvasInstance != null)
        {
            canvasGroup = canvasInstance.GetComponent<CanvasGroup>();
            StartCoroutine(FadeCanvasGroup(1f, 0f, 2f));
            StartCoroutine(FadingOver());
        }

        AttributesPlayer.Instance.CanPlayerMove = true; // TODO - change 
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
    /// Coroutine
    /// Waits till canvas has faded out and become invisible.
    /// Removes pause menu prefab. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadingOver()
    {
        // wait till the canvas is invisible - then remove prefab 
        yield return new WaitUntil(() => canvasGroup.alpha == 0f);
        RemovePrefabFromScene();
    }

    /// <summary>
    /// Adds the pause menu prefab to the scene. 
    /// </summary>
    public void AddPrefabToScene()
    {
        canvasInstance = Instantiate(pausemenuPrefab);
    }

    /// <summary>
    /// Gets the instantiated prefab. 
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstantiatedPrefab()
    {
        return canvasInstance;
    }

    /// <summary>
    /// Removes the instantiated pause menu prefab from the scene. 
    /// </summary>
    public void RemovePrefabFromScene()
    {
        if (GetInstantiatedPrefab() != null)
        {
            Destroy(GetInstantiatedPrefab());
        }
    }
}
