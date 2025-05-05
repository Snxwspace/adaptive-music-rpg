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

    public GameObject frameTop;
    public GameObject frameBottom;

    // Awake is called when the gameObject "wakes up" or becomes loaded
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");
        if(objs.Length > 1) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("O")) {
            if(lastSceneType == "Battle") {
                transform.Rotate(-overworldToBattleRotation, 0, 0);
            }
            // I want the camera movement to be smooth, but idrk how to do that effectively
            transform.position = following.transform.position + offsetTarget;
            lastSceneType = "Overworld";
        } else if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            if(lastSceneType == "Overworld") {
                transform.Rotate(overworldToBattleRotation, 0, 0);
            }
            transform.position = battlePosition;
            lastSceneType = "Battle";
        }
    }
}
