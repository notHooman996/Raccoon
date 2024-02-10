using UnityEngine;

public class TempCameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 3f;
    
    private bool isFirstCamera = true; 
    
    private void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SceneChanger"))
        {
            isFirstCamera = !isFirstCamera;

            if (isFirstCamera)
            {
                CameraHandler.Instance.SwitchCamera(CameraHandler.Instance.GetCameraByName("Camera1"));
            }
            else
            {
                CameraHandler.Instance.SwitchCamera(CameraHandler.Instance.GetCameraByName("Camera2"));
            }
        }
    }

    private void Update()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the camera
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the camera horizontally
        transform.Rotate(Vector3.up, mouseX * turnSpeed);
    }
}
