using Unity.Collections;
using UnityEngine;

namespace GameController
{
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

        public float GetPlayerSpeed()
        {
            return playerSpeed; 
        }
    }
}