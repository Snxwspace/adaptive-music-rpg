using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerMain : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    private Vector3 jumpForce = new Vector3(0, 15, 0);
    private Rigidbody selfRigidbody;
    public float gravityScale = 2.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // grabbing rigidbody from self
        selfRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = normalizeVector(direction);

        transform.Translate(direction * Time.deltaTime * speed);


        if(Input.GetKeyDown(KeyCode.Space)) { // fix midair jumping
            Vector3 velocity = selfRigidbody.GetRelativePointVelocity(selfRigidbody.centerOfMass); // gets rigidbody velocity at the center of mass
            selfRigidbody.AddForce(jumpForce - velocity, ForceMode.VelocityChange); // adds a force equal to and exceeding the current velocity
        }
    }

    // FixedUpdate is like update but for physics calculations
    void FixedUpdate()
    {
        selfRigidbody.AddForce(Physics.gravity * (gravityScale-1) * selfRigidbody.mass);
    }

    Vector3 normalizeVector(Vector3 direction) {
        float xFactor = 1.0f;
        float zFactor = 1.0f;
        if(direction.x != 0) {
            xFactor = math.abs(direction.x);
        }
        if(direction.z != 0) {
            zFactor = math.abs(direction.z);
        }
        direction.x /= xFactor;
        direction.z /= zFactor;
        direction.Normalize();
        direction.x *= xFactor;
        direction.z *= zFactor;
        return direction;
    }
}
