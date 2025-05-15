using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    private StatHandler statHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statHandler = GetComponent<StatHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
