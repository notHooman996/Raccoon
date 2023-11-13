using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; 
    
    private void Start()
    {
        
    }

    private void Update()
    {
        // get input 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // calculate the movement direction 
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        Vector3 movement = direction * speed * Time.deltaTime;
        
        // move the player 
        transform.Translate(movement);
    }
}
