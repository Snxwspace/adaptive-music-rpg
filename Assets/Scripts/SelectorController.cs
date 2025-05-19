using UnityEngine;

public class SelectorController : MonoBehaviour
{
    private BattleHandler battleHandler;
    public int selectedIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject game = GameObject.FindGameObjectWithTag("GameObject");
        battleHandler = game.GetComponent<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
