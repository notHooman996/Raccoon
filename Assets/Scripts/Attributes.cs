using UnityEngine;
using Unity.Collections;

public class Attributes : MonoBehaviour
{
    // singleton 
    public static Attributes Instance;

    [Header("CanPlayerMove")] 
    [SerializeField, ReadOnly] private bool canPlayerMove;
    
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
        canPlayerMove = true; // TODO - set to false when game starts 
    }

    private void Update()
    {
        
    }
    
    public void SetCanPlayerMove()
    {
        canPlayerMove = !canPlayerMove;
    }
    
    public bool GetCanPlayerMove()
    {
        return canPlayerMove; 
    }
}
