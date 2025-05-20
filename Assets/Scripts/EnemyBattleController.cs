using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    private StatHandler statHandler;
    private UpdatePlaceholderUI uiUpdate;
    private BattleHandler battleHandler;

    // [0]name, [1]category(phys/mag/heal/sup), [2]baseChance(toFloat), [3]basePower(toInt), [4]atkMult(toFloat),
    // [5]defMult(toFloat), [6]spDefMult(toFloat), [7]targets(enemy/ally, one/all)
    public string[,] skills; // doesnt work but not my problem atp

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statHandler = GetComponent<StatHandler>();
        uiUpdate = GetComponent<UpdatePlaceholderUI>();

        GameObject game = GameObject.FindGameObjectWithTag("GameObject");
        battleHandler = game.GetComponent<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(statHandler.currentHP <= 0) {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Destroy(uiUpdate.textObject);
        battleHandler.enemyTeam.Remove(gameObject);
        (GameObject, StatHandler) pair = (null, null);
        for(int i = 0; i < battleHandler.allBattlers.Count; i++) {
            (GameObject j, _) = battleHandler.allBattlers[i];
            if(j == gameObject) {
                pair = battleHandler.allBattlers[i];
                battleHandler.allBattlers.RemoveAt(i);
                break;
            }
        }
        battleHandler.turnQueue.Remove(pair);
        battleHandler.fastestEntities.Remove(pair);
    }

    public void TakeBattleTurn() {
        // initializing local variables
        GameObject target;
        StatHandler targetStats;
        List<GameObject> enemyTeam;

        if(battleHandler.allyTeam.Contains(gameObject)) {
            enemyTeam = battleHandler.enemyTeam;
        } else {
            enemyTeam = battleHandler.allyTeam;
        }

        int index = Random.Range(0, enemyTeam.Count);
        target = enemyTeam[index];
        targetStats = target.GetComponent<StatHandler>();

        float rand = Random.value;
        if(rand < 0.4) {
            if(statHandler.currentMP >= 5) {
                statHandler.currentMP -= 5;
                int damage = AttackGeneric((int)statHandler.magic, (int)targetStats.defense, (int)targetStats.magic, 0.25f, 0, 0.2f, 12);
                float temp = damage * targetStats.magicDamageMultiplier;
                damage = (int)temp;
                targetStats.currentHP -= damage;
                Invoke(nameof(EndTurn), 0.5f);
            } else {
                Debug.Log("Not enough MP!");
                Invoke(nameof(EndTurn), 0.5f);
            }
        } else { // basic attack
            int damage = AttackBasic((int)statHandler.attack, (int)targetStats.defense);
            float temp = damage * targetStats.physDamageMultiplier;
            damage = (int)temp;
            targetStats.currentHP -= damage;
            Invoke(nameof(EndTurn), 0.5f);
        }
    }

    public int AttackBasic(int attack, int targetDefense, float attackMult = 0.15f, float defenseMult = 0.25f) {
        float damage = 8;  
        damage += attack * attackMult;
        damage -= targetDefense * defenseMult;
        return (int)damage;
    }

    public int AttackGeneric(int playerAttack, int enemyDefense, int enemyMagic, float attackMult, 
                             float defenseMult, float magicDefMult, int baseDamage)
    {
        // playerAttack can be the player's magic stat if the attack is a magic attack
        float damage = baseDamage;
        damage += playerAttack * attackMult;
        damage -= (enemyDefense * defenseMult) + (enemyMagic * magicDefMult);
        return (int)damage;
    }

    private void EndTurn() {    // just here so the end of the turn has functionality
        battleHandler.isTurnFinished = true;
    }
}
