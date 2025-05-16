using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject frame;

    void Awake()
    {
        // if it's not the only player object, destroy itself
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Canvas");
        if(objs.Length > 1) {
            Destroy(gameObject);
        }

        // if it's not the only canvas object, make it so it won't be lost between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            if(!frame.activeSelf) {
                frame.SetActive(true);
            }
        }
    }
}
