using System.ComponentModel;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Vector3 cameraDirection;

    public bool DoStageBillboarding { get; set; } = true; // TODO - change later 
    public bool DoSpriteBillboarding { get; set; } // TODO - set with BackdropTool 
    
    private void Update()
    {
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
