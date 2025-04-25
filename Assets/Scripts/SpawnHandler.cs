using UnityEditor.Rendering;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public float[] spawnChances; // in percent
    public GameObject[] spawnedIn;
    
    public float spawnRangeX = 0.0f;
    public float spawnRangeZ = 0.0f;

    public float spawnIntervalMin = 1.0f;
    public float spawnIntervalMax = 1.0f;

    private int firstMissingIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsInvoking(nameof(SpawnRandom))) {
            firstMissingIndex = CheckNeededSpawns();
            if(firstMissingIndex != -1) {
                Invoke(nameof(SpawnRandom), Random.Range(spawnIntervalMin, spawnIntervalMax));
            }
        }
    }

    int CheckNeededSpawns() {
        for(int i = 0; i < spawnedIn.Length; i++) {
            try
            {
                bool temp = spawnedIn[i].activeSelf;
            }
            catch (UnassignedReferenceException)
            {
                return i;
            }
            catch (MissingReferenceException)
            {
                return i;
            }
        }
        return -1;
    }

    void SpawnRandom() {
        // Randomizing which entity to spawn, TODO
        int spawnIndex = 0;

        Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
                                       transform.position.y,
                                       transform.position.z + Random.Range(-spawnRangeZ, spawnRangeZ));
    
        spawnedIn[firstMissingIndex] = Instantiate(spawnPrefabs[spawnIndex], spawnPos, spawnPrefabs[spawnIndex].transform.rotation);
    }
}
