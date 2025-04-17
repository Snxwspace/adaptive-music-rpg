using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControllerMain : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    public float jumpSpeed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        // consider making the speed stay equalized for whatever angle you're travelling at
        // how though...? 
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space)) { // fix midair jumping
            // add velocity
        }
    }
}
