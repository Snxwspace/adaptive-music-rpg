using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    public GameObject battleMenu;
    public BattleMenuController battleMenuScript;
    public GameObject selectorArrow;
    public SelectorController selectorScript;
    public Actions currentAction;

    // readonly enums yay
    public enum Actions {
        Idle = -1, // not current turn
        Waiting = 0, // current turn, in menu
        BasicAttack = 18, // selecting target for basic attack
        SkillMenu = 20, // selecting skill/magic to use
        SkillTarget = 28, // selecting target of skill/magic
        ItemMenu = 30, // in inventory menu
        ItemInformation = 31, // checking what an item does
        ItemTarget = 38, // choosing target of item
        GuardConfirm = 40, // confirming whether to guard
        RunConfirm = 50, // confirming whether to run
        SpecialMenu = 90, // in the special part of the menu, no clue what it does rn lol
    }   // why did i explicitly put numbers its not actually useful what am i doing

    void Start()
    {
        battleMenuScript = battleMenu.GetComponent<BattleMenuController>();
        selectorScript = selectorArrow.GetComponent<SelectorController>();
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
