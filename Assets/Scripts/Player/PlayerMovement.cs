using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (AttributesPlayer.Instance.CanPlayerMove)
        {
            if (InputHandler.Instance.GetMovementInput() != (0, 0))
            {
                Move();
            }
        }
    }

    private void Move()
    {
        // get input 
        float horizontalInput = InputHandler.Instance.GetMovementInput().x;
        float verticalInput = InputHandler.Instance.GetMovementInput().y;
        
        // calculate the movement direction 
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        Vector3 movement = CameraRelativeMovement(direction) * AttributesPlayer.Instance.GetPlayerSpeed() * Time.deltaTime;
        movement.y = 0; // make sure it does not fly or fall 
        
        // move the player 
        transform.Translate(movement);
    }

    private Vector3 CameraRelativeMovement(Vector3 inputDirection)
    {
        // set temp value 
        Vector3 cameraRelativeDirection = Vector3.zero;

        // vertical 
        if (inputDirection.z > 0 || inputDirection.z == 1)
        {
            cameraRelativeDirection += DirectionVector();
        }
        else if (inputDirection.z < 0 || inputDirection.z == -1)
        {
            cameraRelativeDirection += DirectionVector() * -1; 
        }
        
        // horizontal 
        if (inputDirection.x > 0 || inputDirection.x == 1)
        {
            cameraRelativeDirection += RightVector();
        }
        else if (inputDirection.x < 0 || inputDirection.x == -1)
        {
            cameraRelativeDirection += RightVector() * -1; 
        }
        
        // return the normalized direction vector 
        return cameraRelativeDirection.normalized; 
    }

    private Vector3 DirectionVector()
    {
        // calculate the direction vector between the two points 
        Vector3 directionVector = transform.position - CameraHandler.Instance.GetCurrentCamera().transform.position;

        // normalize the vector to get its unit vector 
        Vector3 normalizedVector = Vector3.Normalize(directionVector);

        return normalizedVector;
    }

    private Vector3 RightVector()
    {
        // get direction vector 
        Vector3 directionVector = DirectionVector();
        
        // rotate the direction vector by 90 degrees to the right 
        Vector3 rightVector = new Vector3(directionVector.z, directionVector.y, -directionVector.x);

        return rightVector;
    }
}
