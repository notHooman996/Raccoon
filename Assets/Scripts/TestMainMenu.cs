using UnityEngine;

public class TestMainMenu : MonoBehaviour
{
    public GameObject mainmenuPrefab; // reference to the canvas ui prefab 
    private GameObject canvasInstance; // reference to the instantiated prefab 
    private bool isMenu = false; 
    
    private void Start()
    {
        mainmenuPrefab.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMenu = !isMenu;

            if (isMenu)
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
        canvasInstance = Instantiate(mainmenuPrefab);
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
