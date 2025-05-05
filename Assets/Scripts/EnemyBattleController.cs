using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    // battle stats
    public int level = 0;   // is this neccesary? no clue lol
    public int maxHP = 0;
    public float currentHP;
    public int maxMP = 0;   // anti spell spam
    public float currentMP;
    public int attack = 0;
    public int defense = 0;
    public int magic = 0;
    public int speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
