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
        // checks to see if there is not a current spawn delay happening
        if(!IsInvoking(nameof(SpawnRandom))) {
            // checks to see if anything needs spawning and at what index does it need that (in a global variable because im a c# noob)
            firstMissingIndex = CheckNeededSpawns();
            // if the index returned is not -1, the code for "nothing needs spawning", spawn a random enemy after a random delay
            if(firstMissingIndex != -1) {
                Invoke(nameof(SpawnRandom), Random.Range(spawnIntervalMin, spawnIntervalMax));
            }
        }
    }

    int CheckNeededSpawns() {
        // runs through every possible indexing of the list of enemies spawned by this spawner
        for(int i = 0; i < spawnedIn.Length; i++) {
            try {
                // uses an object method that doesn't work on None objects to try to error out
                bool temp = spawnedIn[i].activeSelf;
            } catch (UnassignedReferenceException) {
                // if an index is unnassigned (there was never an object in that position), return that index as one in need of spawns
                return i;
            } catch (MissingReferenceException) {
                // if a reference is missing (the GameObject contained within it was destroyed), return that index as one in need of spawns
                return i;
            }
            // do not try to catch any other errors-- surpressing all errors is a recipe for disaster
        }
        // if every index worked perfectly, return -1 as a sign that everything's fine
        return -1;
    }

    void SpawnRandom() {
        // Randomizing which entity to spawn, TODO
        int spawnIndex = 0;

        // spawns an object within a range of the spawner
        Vector3 spawnPos = new(transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
                               transform.position.y,
                               transform.position.z + Random.Range(-spawnRangeZ, spawnRangeZ));
    
        spawnedIn[firstMissingIndex] = Instantiate(spawnPrefabs[spawnIndex], spawnPos, spawnPrefabs[spawnIndex].transform.rotation);
    }
}
