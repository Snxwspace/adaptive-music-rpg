using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject frame;
    public GameObject HPMPcontainer;

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
        } else {
            if(frame.activeSelf) {
                frame.SetActive(false);
            }
        }
    }

    public GameObject InstantiateNewHPText(GameObject prefab) {    // i just need to throw something together this wouldnt stay for long
        return Instantiate(prefab, new Vector3(808.75f, 456.75f), prefab.transform.rotation, HPMPcontainer.transform);
    }
}
