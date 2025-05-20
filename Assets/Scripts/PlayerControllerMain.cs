using System;
using Unity.Mathematics;
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


    // internal use variables
    private Rigidbody selfRigidbody;
    public float groundCheckSensitivity = 0.5f;
    public float groundCheckWidth = 0.45f;
    private UsefulFunctions functions;
    private Vector3 overworldPosition;
    private quaternion overworldRotation;
    private string lastSceneType;
    private BattleHandler battleHandler;
    private StatHandler statHandler;
    private PlayerBattleController battleFunctions;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // grabbing rigidbody from self
        selfRigidbody = GetComponent<Rigidbody>();

        // grabbing scripts from self
        statHandler = GetComponent<StatHandler>();
        battleFunctions = GetComponent<PlayerBattleController>();
        
        // this is needed to gain access to the useful functions
        functions = gameObject.AddComponent<UsefulFunctions>();

        // need to access the battle handler script
        GameObject game = GameObject.FindGameObjectWithTag("GameObject");
        battleHandler = game.GetComponent<BattleHandler>();
    }

    // Awake is called once when the object becomes loaded
    void Awake()
    {
        // if it's not the only player object, destroy itself
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if(objs.Length > 1) {
            Destroy(gameObject);
        }

        // if it's not the only player object, make it so it won't be lost between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("O")) {    // checks if the active scene is an overworld scene
            if (lastSceneType == "Battle") {
                // if the player is coming out of a battle, reset the location and rotation to the values when starting the battle
                Debug.Log("Exiting battle...");
                transform.SetPositionAndRotation(overworldPosition, overworldRotation);
            }

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            // sets a vector to move at, then normalizes it to avoid diagonal movement being ~30% faster than it should be
            Vector3 direction = new(horizontalInput, 0, verticalInput);
            direction = functions.NormalizeMoveVector(direction);

            transform.Translate(walkSpeed * Time.deltaTime * direction);


            if(Input.GetKeyDown(KeyCode.Space)) { 
                if(functions.IsGrounded(groundCheckSensitivity, groundCheckWidth)) {    // fixes midair jumping
                    Vector3 velocity = selfRigidbody.GetRelativePointVelocity(selfRigidbody.centerOfMass); // gets rigidbody velocity and stores it
                    selfRigidbody.AddForce(jumpForce - velocity, ForceMode.VelocityChange); // adds a force to neutralize the current velocity, and sets a base jumping velocity
                }
            }

            lastSceneType = "Overworld";
        } else if(SceneManager.GetActiveScene().name.StartsWith("B")) {     // checks if active scene is a battle scene
            if(lastSceneType == "Overworld") {
                // checks if the player is entering a battle coming from the overworld
                // saving position and rotation of the overworld to be loaded later
                overworldPosition = gameObject.transform.position;
                overworldRotation = gameObject.transform.rotation;
                // sets the player's position to the designated position where they should be battling at
                gameObject.transform.position = battleLocation;
                // sets up the battle handler to add this to the good guy team
                battleHandler.allyTeam.Add(gameObject);
                battleHandler.isInitialized = false;
            }
            switch (battleFunctions.currentAction) {
                case PlayerBattleController.Actions.Waiting:
                    if(!battleFunctions.battleMenu.activeSelf) {
                        battleFunctions.battleMenu.SetActive(true);
                    }

                    if(Input.GetKeyDown(KeyCode.Return)) {
                        switch(Array.IndexOf(battleFunctions.battleMenuScript.selectionSprites, 
                                            battleFunctions.battleMenuScript.imageLoader.sprite)) {
                            case 0:
                                battleFunctions.currentAction = PlayerBattleController.Actions.BasicAttack;
                                battleFunctions.battleMenu.SetActive(false);
                                break;
                            case 1:
                                battleFunctions.currentAction = PlayerBattleController.Actions.SkillMenu;
                                break;
                            case 2:
                                // loud incorrect buzzer
                                break;
                            case 3:
                                // battleFunctions.currentAction = PlayerBattleController.Actions.GuardConfirm;
                                // need to push this out a little bit but i dont want to put in the effort to do that atm
                                statHandler.magicDamageMultiplier *= battleFunctions.guardDamageMult;
                                statHandler.physDamageMultiplier *= battleFunctions.guardDamageMult;
                                battleFunctions.battleMenu.SetActive(false);
                                battleFunctions.currentAction = PlayerBattleController.Actions.Guarding;
                                battleHandler.isTurnFinished = true;
                                break;
                            case 4:
                                SceneManager.LoadSceneAsync("Overworld_Prototype1");
                                break;
                            case 5:
                                // loud incorrect buzzer
                                break;
                        }
                    }
                    break;
                case PlayerBattleController.Actions.BasicAttack:
                    if(!battleFunctions.selectorArrow.activeSelf) {
                        battleFunctions.selectorArrow.SetActive(true);
                    }

                    if(Input.GetKeyDown(KeyCode.Return)) {
                        int index = battleFunctions.selectorScript.selectedIndex;
                        GameObject enemyObject = battleHandler.enemyTeam[index];
                        StatHandler enemyStats = enemyObject.GetComponent<StatHandler>();
                        int damage = battleFunctions.AttackBasic((int)statHandler.attack, (int)enemyStats.defense);
                        float damageF = damage * enemyStats.physDamageMultiplier;
                        enemyStats.currentHP -= (int)damageF;
                        battleFunctions.currentAction = PlayerBattleController.Actions.Idle;
                        battleFunctions.selectorArrow.SetActive(false);
                        battleHandler.isTurnFinished = true;
                        break;
                    } else if (Input.GetKeyDown(KeyCode.Escape)) {
                        battleFunctions.currentAction = PlayerBattleController.Actions.Waiting;
                        battleFunctions.selectorArrow.SetActive(false);
                    }
                    break;
            }
            
            lastSceneType = "Battle";
        }
    }

    // FixedUpdate is like update but for physics calculations
    void FixedUpdate()
    {
        // amplifies gravity slightly to avoid really floaty really strong jumps
        selfRigidbody.AddForce((gravityScale-1) * selfRigidbody.mass * Physics.gravity);
    }

    // OnTriggerEnter gets called once when the object enters the collider of another object
    void OnTriggerEnter(Collider other)
    {
        // checks if the player is colliding with specifically an enemy
        if(other.CompareTag("Enemy")) {
            Debug.Log("Enemy encountered!");
            // checks if the current scene is the overworld
            if(SceneManager.GetActiveScene().name.StartsWith("O")) {
                // do the scene switch handler and transition
                SceneManager.LoadSceneAsync("Battle_Prototype1");
                // keeps the enemy it collided with intact through the transition so it can spawn the battle enemies
                DontDestroyOnLoad(other.gameObject);
            }
        }
    }
}
