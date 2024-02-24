using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameData gameData;

    private void Start()
    {
        Debug.Log("Hello");
        int sceneIndex = gameData.sceneIndex; 
    }
}