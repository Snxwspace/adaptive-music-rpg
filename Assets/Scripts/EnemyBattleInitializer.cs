using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBattleInitializer : MonoBehaviour
{
    public GameObject[] enemySpawnPrefabs;
    private BattleHandler battleHandler;

    void Start()
    {
        GameObject game = GameObject.FindGameObjectWithTag("GameObject");
        battleHandler = game.GetComponent<BattleHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // wait until the scene is a battle scene to do anything-- don't want to spawn explicit combat enemies outside of combat
        if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            transform.position = new Vector3(0, -10000, 0); // no one will ever find us here
            // spawn every enemy on the spawnin list then destroy itself
            for(int i = 0; i < enemySpawnPrefabs.Length; i++) {
                // spawn the objects at a y value so that it doesn't clip into the floor and an x value where it takes its own side of the battle arena
                float spawnY = enemySpawnPrefabs[i].transform.position.y; 
                GameObject spawned = Instantiate(enemySpawnPrefabs[i], new Vector3(3*(i+1), spawnY, 0), enemySpawnPrefabs[i].transform.rotation);
                // sets up the battle handler to set everything added to the enemy team
                battleHandler.enemyTeam.Add(spawned);
                battleHandler.isInitialized = false;
            }
            Destroy(gameObject);
        }
    }
}
