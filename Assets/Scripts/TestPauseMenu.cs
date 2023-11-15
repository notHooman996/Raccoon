using UnityEngine;

public class TestPauseMenu : MonoBehaviour
{
    public GameObject pausemenuPrefab; // reference to the canvas ui prefab 
    private GameObject canvasInstance; // reference to the instantiated prefab 
    private bool isPaused = false; 
    
    private void Start()
    {
        pausemenuPrefab.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                AddPrefabToScene();
            }
            else
            {
                RemovePrefabFromScene();
            }
        }
    }

    public void AddPrefabToScene()
    {
        canvasInstance = Instantiate(pausemenuPrefab);
    }

    public GameObject GetInstantiatedPrefab()
    {
        return canvasInstance;
    }

    public void RemovePrefabFromScene()
    {
        if (GetInstantiatedPrefab() != null)
        {
            Destroy(GetInstantiatedPrefab());
        }
    }
}
