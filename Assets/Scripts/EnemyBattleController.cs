using Unity.VisualScripting;
using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    private StatHandler statHandler;
    private UpdatePlaceholderUI uiUpdate;
    private BattleHandler battleHandler;

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
}
