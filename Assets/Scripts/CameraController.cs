using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Vector3 offsetTarget = new Vector3(0, 4, -9);
    public GameObject following;
    public Vector3 battlePosition = new Vector3(0, 3, -9);
    public float overworldToBattleRotation = -10.0f;
    private string lastSceneType;

    // variables to handle the frames, for transitions and cinematic cameras
    public GameObject frameTop;
    public GameObject frameBottom;

    // Awake is called when the gameObject "wakes up" or becomes loaded
    void Awake()
    {
        // checks if there's more than one object tagged with the main camera tag; if so, destroy itself as it's unneeded
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");
        if(objs.Length > 1) {
            Destroy(gameObject);
        }
        
        // if it's the only main camera object in the current scene, don't let it get destroyed when moving scenes
        DontDestroyOnLoad(gameObject);
    }

    // LateUpdate is called once per frame after most Update steps-- this is useful here to avoid camera jittering
    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("O")) {
            if(lastSceneType == "Battle") {
                // fix the rotation coming out of a battle
                transform.Rotate(-overworldToBattleRotation, 0, 0);
            }
            // I want the camera movement to be smooth, but idrk how to do that effectively
            transform.position = following.transform.position + offsetTarget;
            lastSceneType = "Overworld";
        } else if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            if(lastSceneType == "Overworld") {
                // rotate the camera for a better battle shot once entering battle
                transform.Rotate(overworldToBattleRotation, 0, 0);
            }
            transform.position = battlePosition;
            lastSceneType = "Battle";
        }
    }
}
