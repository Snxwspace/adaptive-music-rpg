using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerMain : MonoBehaviour
{
    // movement variables
    private float horizontalInput;
    private float verticalInput;
    public float walkSpeed = 10.0f;
    private Vector3 jumpForce = new Vector3(0, 15, 0);
    public float gravityScale = 2.0f;
    public Vector3 battleLocation = new Vector3(-3, 1, 0);

    // combat variables
    public int level = 1;
    public float nextXP = 100;
    public float currentXP = 0.0f;
    public float maxHP = 70;
    public float currentHP = 70;
    public float maxMP = 40;
    public float currentMP = 40;
    public float attack = 6;
    public float defense = 8;
    public float magic = 16;
    public float speed = 12;

    // internal use variables
    private Rigidbody selfRigidbody;
    public float groundCheckSensitivity = 0.5f;
    public float groundCheckWidth = 0.45f;
    private UsefulFunctions functions;
    private Vector3 overworldPosition;
    private quaternion overworldRotation;
    private string lastSceneType;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // grabbing rigidbody from self
        selfRigidbody = GetComponent<Rigidbody>();
        
        functions = gameObject.AddComponent<UsefulFunctions>();
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if(objs.Length > 1) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("O")) {
            if (lastSceneType == "Battle") {
                Debug.Log("Exiting battle...");
                // Debug.Log($"Stored position is {overworldLocationData.position}.");
                transform.SetPositionAndRotation(overworldPosition, overworldRotation);
            }

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
            direction = functions.NormalizeMoveVector(direction);

            transform.Translate(walkSpeed * Time.deltaTime * direction);


            if(Input.GetKeyDown(KeyCode.Space)) { 
                if(functions.IsGrounded(groundCheckSensitivity, groundCheckWidth)) {    // fixing midair jumping
                    Vector3 velocity = selfRigidbody.GetRelativePointVelocity(selfRigidbody.centerOfMass); // gets rigidbody velocity at the center of mass
                    selfRigidbody.AddForce(jumpForce - velocity, ForceMode.VelocityChange); // adds a force equal to and exceeding the current velocity
                }
            }

            lastSceneType = "Overworld";
        } else if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            if(lastSceneType == "Overworld") {
                // saving position and rotation of the overworld to be loaded later
                overworldPosition = gameObject.transform.position;
                overworldRotation = gameObject.transform.rotation;
                // Debug.Log($"Saving position {overworldLocationData.position}...");
                gameObject.transform.position = battleLocation;
            }
            lastSceneType = "Battle";
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
            if(SceneManager.GetActiveScene().name.StartsWith("O")) {
                // do the scene switch handler and transition
                SceneManager.LoadSceneAsync("Battle_Prototype1");
                DontDestroyOnLoad(other.gameObject);
            }
        }
    }
}
