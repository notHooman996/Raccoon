using System;
using Unity.Collections;
using UnityEngine; 

public class InputHandler : MonoBehaviour
{
    // singleton 
    public static InputHandler Instance; 
    
    [Header("MoveControls")] 
    [SerializeField, ReadOnly] private float horizontal;
    [SerializeField, ReadOnly] private float vertical;

    [Header("InteractControls")] 
    [SerializeField, ReadOnly] private bool interact;

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

    private void Update()
    {
        // set input 
        SetMovementInput();
        SetInteractInput();
    }

    private void SetMovementInput()
    {
        (horizontal, vertical) = (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void SetInteractInput()
    {
        interact = Input.GetButtonDown("Interact");
    }

    public (float x, float y) GetMovementInput()
    {
        return (horizontal, vertical);
    }

    public bool GetInteractInput()
    {
        return interact;
    }
}
