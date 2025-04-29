using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerMain : MonoBehaviour
{
    // movement variables
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    private Vector3 jumpForce = new Vector3(0, 15, 0);
    public float gravityScale = 2.0f;

    // internal use variables
    private Rigidbody selfRigidbody;
    public float groundCheckSensitivity = 0.5f;
    public float groundCheckWidth = 0.45f;
    private UsefulFunctions functions;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // grabbing rigidbody from self
        selfRigidbody = GetComponent<Rigidbody>();
        
        functions = gameObject.AddComponent<UsefulFunctions>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = functions.NormalizeMoveVector(direction);

        transform.Translate(direction * Time.deltaTime * speed);


        if(Input.GetKeyDown(KeyCode.Space)) { 
            if(functions.IsGrounded(groundCheckSensitivity, groundCheckWidth)) {    // fixing midair jumping
                Vector3 velocity = selfRigidbody.GetRelativePointVelocity(selfRigidbody.centerOfMass); // gets rigidbody velocity at the center of mass
                selfRigidbody.AddForce(jumpForce - velocity, ForceMode.VelocityChange); // adds a force equal to and exceeding the current velocity
            }
        }
    }

    // FixedUpdate is like update but for physics calculations
    void FixedUpdate()
    {
        selfRigidbody.AddForce((gravityScale-1) * selfRigidbody.mass * Physics.gravity);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) {
            Debug.Log("Enemy encountered!");
        }
    }
}
