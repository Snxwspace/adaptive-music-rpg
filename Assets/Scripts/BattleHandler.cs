using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{
    public List<GameObject> allyTeam;
    public List<GameObject> enemyTeam;
    public List<(GameObject, StatHandler)> allBattlers;   // should i set this to hold an array[2] to perm store stat handler? 
    public List<(GameObject, StatHandler)> turnQueue;
    public List<(GameObject, StatHandler)> fastestEntities;
    private int highestSpeed = 0;
    private GameObject currentTurn;
    public bool isInitialized = false;
    public bool isAddingReinforcements = false;
    public bool isTurnFinished = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        allBattlers = new List<(GameObject, StatHandler)>(0);
        turnQueue = new List<(GameObject, StatHandler)>(0);
        fastestEntities = new List<(GameObject, StatHandler)>(0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            if(!isInitialized) {
                InitializeLists(!isAddingReinforcements);
                isInitialized = true;
            }
            if(isTurnFinished) {
                if(enemyTeam.Count == 0 || allyTeam.Count == 0) {
                    EndCombat();
                }

                if(turnQueue.Count == 0) {
                    ResetRound();
                }
                currentTurn = GetNextTurn(out StatHandler query);
                Debug.Log("It is now " + currentTurn.name + "\'s turn.");
                // figure out how to have the others take their turns
                if(query.isPlayerControlled) {
                    PlayerBattleController battleController = currentTurn.GetComponent<PlayerBattleController>();
                    battleController.ToggleBattleMenu();
                    battleController.currentAction = PlayerBattleController.Actions.Waiting;
                } else {/* todo */}
                
                isTurnFinished = false;
            }
        }
    }

    private void InitializeLists(bool needsRoundReset = true) {
        allBattlers.Clear();
        for(int i = 0; i < allyTeam.Count; i++) {
            StatHandler j = allyTeam[i].GetComponent<StatHandler>();
            allBattlers.Add((allyTeam[i], j));
        }
        for(int i = 0; i < enemyTeam.Count; i++) {
            StatHandler j = enemyTeam[i].GetComponent<StatHandler>();
            allBattlers.Add((enemyTeam[i], j));
        }

        if(needsRoundReset) {
            ResetRound();
        }
    }

    private void ResetRound() {
        turnQueue.Clear();
        turnQueue.AddRange(allBattlers);
    }

    private GameObject GetNextTurn(out StatHandler stat) {
        if(fastestEntities.Count == 0) {
            highestSpeed = 0;
            for(int i = 0; i < turnQueue.Count; i++) {
                int speedStat = GetSpeed(turnQueue[i]);

                if(speedStat > highestSpeed) {
                    highestSpeed = speedStat;
                    fastestEntities.Clear();
                    fastestEntities.Add(turnQueue[i]);
                } else if(speedStat == highestSpeed) {
                    fastestEntities.Add(turnQueue[i]);
                }
            }
        }

        int randIndex = UnityEngine.Random.Range(0, fastestEntities.Count);
        // Debug.Log(randIndex);
        // Debug.Log(fastestEntities.Count);
        (GameObject, StatHandler) nextTurn = fastestEntities[randIndex];
        turnQueue.Remove(nextTurn);
        fastestEntities.Remove(nextTurn);
        GameObject next;
        (next, stat) = nextTurn;
        return next;
    }

    public int GetSpeed((GameObject, StatHandler) entity) {
        // let's be honest, only reason this is a seperate function is because 
        // it's going to be a hot function that'll get refactored over and over again
        // seriously i have no clue what im doing here send help

        (_, StatHandler stat) = entity;
        return (int)stat.speed;
    }

    public void EndCombat() {
        isInitialized = false;
        isAddingReinforcements = false;
        isTurnFinished = true;
        enemyTeam.Clear();
        highestSpeed = 0;
        currentTurn.SendMessage("GUIClear");
        SceneManager.LoadSceneAsync("Overworld_Prototype1");
    }
}
