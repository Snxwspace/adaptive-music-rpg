using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offsetTarget = new Vector3(0, 4, -9);
    public GameObject following;

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
        // I want the camera movement to be smooth, but idrk how to do that effectively
        transform.position = following.transform.position + offsetTarget;
    }
}
