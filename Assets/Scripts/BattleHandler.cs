using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{
    public List<GameObject> allyTeam;
    public List<GameObject> enemyTeam;
    private List<GameObject> allBattlers;
    private List<GameObject> turnQueue;
    private List<GameObject> fastestEntities;
    private int highestSpeed = 0;
    private GameObject currentTurn;
    public bool isInitialized = false;
    public bool isAddingReinforcements = false;
    public bool isTurnFinished = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        allBattlers = new List<GameObject>(0);
        turnQueue = new List<GameObject>(0);
        fastestEntities = new List<GameObject>(0);
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
                if(turnQueue.Count == 0) {
                    ResetRound();
                }
                currentTurn = GetNextTurn();
                Debug.Log("It is now " + currentTurn.name + "\'s turn.");
                // figure out how to have the others take their turns
                isTurnFinished = false;
            }
        }
    }

    private void InitializeLists(bool needsRoundReset = true) {
        allBattlers.Clear();
        allBattlers.AddRange(allyTeam);
        allBattlers.AddRange(enemyTeam);

        if(needsRoundReset) {
            ResetRound();
        }
    }

    private void ResetRound() {
        turnQueue.Clear();
        turnQueue.AddRange(allBattlers);
    }

    private GameObject GetNextTurn() {
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
        GameObject nextTurn = fastestEntities[randIndex];
        turnQueue.Remove(nextTurn);
        fastestEntities.Remove(nextTurn);
        return nextTurn;
    }

    public int GetSpeed(GameObject entity) {
        // let's be honest, only reason this is a seperate function is because 
        // it's going to be a hot function that'll get refactored over and over again
        // seriously i have no clue what im doing here send help
        return 1;
    }
}
