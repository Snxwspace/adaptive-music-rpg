using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBattleInitializer : MonoBehaviour
{
    public GameObject[] enemySpawnPrefabs;
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("B")) {
            transform.position = new Vector3(0, -10000, 0); // no one will ever find us here
            // spawn the list then explode yourself
            for(int i = 0; i < enemySpawnPrefabs.Length; i++) {
                float spawnY = enemySpawnPrefabs[i].transform.position.y; 
                Instantiate(enemySpawnPrefabs[i], new Vector3(3*(i+1), spawnY, 0), enemySpawnPrefabs[i].transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
