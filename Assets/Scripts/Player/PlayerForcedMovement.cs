using System;
using UnityEngine;

public class PlayerForcedMovement : MonoBehaviour
{
    public static Vector3 ForcedPosition { get; set; }

    public static bool ShouldMoveForced { get; set; } = false; 

    private void Update()
    {
        if (ShouldMoveForced)
        {
            ForcedMove();
        }
    }

    private void ForcedMove()
    {
        if (Vector3.Distance(transform.position, ForcedPosition) <= 1.5f)
        {
            ShouldMoveForced = false; 
        }
        else
        {
            transform.LookAt(new Vector3(ForcedPosition.x, transform.position.y, ForcedPosition.z));
            transform.Translate(Vector3.forward * AttributesPlayer.Instance.GetPlayerSpeed() * Time.deltaTime);
        }
    }
}