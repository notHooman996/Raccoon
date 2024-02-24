using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Unity.Collections;
using UnityEngine.SceneManagement;

public class Attributes : MonoBehaviour
{
    // singleton 
    public static Attributes Instance;
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
}
