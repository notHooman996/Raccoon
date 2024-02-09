using System.ComponentModel;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Vector3 cameraDirection;
    [SerializeField] private bool doSpriteBillboarding; 

    public bool DoStageBillboarding { get; set; } // TODO - change later 

    public bool DoSpriteBillboarding
    {
        get { return doSpriteBillboarding; }
        set { doSpriteBillboarding = value; }
    }
    
    private void Update()
    {
        // only do billboarding in the current stage 
        if (DoStageBillboarding)
        {
            if (DoSpriteBillboarding)
            {
                cameraDirection = Camera.main.transform.forward;
                cameraDirection.y = 0;

                transform.rotation = Quaternion.LookRotation(cameraDirection);
            }
        }
    }
}
