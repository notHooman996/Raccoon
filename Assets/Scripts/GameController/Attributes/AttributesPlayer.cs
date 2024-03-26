using System;
using Unity.Collections;
using UnityEngine;

public class AttributesPlayer : MonoBehaviour
{
    // singleton 
    public static AttributesPlayer Instance;
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

    [Header("ActiveVirtualCamera")] 
    [SerializeField, ReadOnly] private float playerSpeed = 5f;
    
    [Header("CanPlayerMove")] 
    [SerializeField, ReadOnly] private bool canPlayerMove;

    [Header("CanPlayerInteract")]
    [SerializeField, ReadOnly] private bool canPlayerInteract = false; 
    
    [Header("PlayerInteractDistance")]
    [SerializeField, ReadOnly] private float interactDistance = 3; 

    private void Start()
    {
        canPlayerMove = true; // TODO - set to false when game starts 
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed; 
    }
    
    public bool CanPlayerMove
    {
        get { return canPlayerMove; }
        set { canPlayerMove = value; }
    }

    public bool CanPlayerInteract
    {
        get { return canPlayerInteract; }
        set { canPlayerInteract = value; }
    }
    
    public float InteractDistance
    {
        get { return interactDistance; }
    }
}