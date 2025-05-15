using UnityEngine;
using UnityEngine.SceneManagement;

public class josephs_script : MonoBehaviour
{
    private GameObject game;
    private BattleHandler battleHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = GameObject.Find("GameObject");
        battleHandler = game.GetComponent<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // joseph saves infinite battles in testing with the power of the backslash
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            SceneManager.LoadSceneAsync("Overworld_Prototype1");
        }

        // joseph progresses testing battles with the power of the right square bracket
        if (Input.GetKeyDown(KeyCode.RightBracket)) {
            battleHandler.isTurnFinished = true;
        }
    }
}

// debug/testing script lol