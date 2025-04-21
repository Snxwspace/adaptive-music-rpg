using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offsetTarget = new Vector3(0, 4, -9);
    public GameObject following;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // I want the camera movement to be smooth, but idrk how to do that effectively
        transform.position = following.transform.position + offsetTarget;
    }
}
