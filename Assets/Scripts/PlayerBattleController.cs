using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    public GameObject battleMenu;
    public BattleMenuController battleMenuScript;

    void Start()
    {
        battleMenuScript = battleMenu.GetComponent<BattleMenuController>();
    }

    public void ToggleBattleMenu()
    {
        battleMenu.SetActive(!battleMenu.activeSelf);
    }

    public int AttackBasic(int playerAttack, int enemyDefense, float attackMult = 0.2f, float defenseMult = 0.25f)
    {    // i need to figure out how to pull the enemy defense helppp
        float damage = 10;  // for basic attacks: baseDamage = 10; (i think)
        damage += playerAttack * attackMult;
        damage -= enemyDefense * defenseMult;
        return (int)damage;
    }

    public int AttackGeneric(int playerAttack, int enemyDefense, int enemyMagic, float attackMult, float defenseMult, float magicDefMult, int baseDamage)
    {
        // playerAttack can be the player's magic stat if the attack is a magic attack
        float damage = baseDamage;
        damage += playerAttack * attackMult;
        damage -= (enemyDefense * defenseMult) + (enemyMagic * magicDefMult);
        return (int)damage;
    }
}
