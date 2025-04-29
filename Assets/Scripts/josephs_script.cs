using UnityEngine;
using UnityEngine.SceneManagement;

public class josephs_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            SceneManager.LoadSceneAsync("Overworld_Prototype1");
        }
    }
}

// debug/testing script lol