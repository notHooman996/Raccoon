using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (AttributesPlayer.Instance.CanPlayerMove)
        {
            Move();
        }
    }

    private void Move()
    {
        // get input 
        float horizontalInput = InputHandler.Instance.GetMovementInput().x;
        float verticalInput = InputHandler.Instance.GetMovementInput().y;
        
        // calculate the movement direction 
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        Vector3 movement = direction * AttributesPlayer.Instance.GetPlayerSpeed() * Time.deltaTime;
        
        // move the player 
        transform.Translate(movement);
    }
}
