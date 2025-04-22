using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private Vector3 direction;
    public float minimumDirectionTime = 4.0f;
    public float maximumDirectionTime = 7.0f;
    // private float timeElapsed;
    private float randomTime;
    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ChangeDirection", 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void ChangeDirection() {
        direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        direction.Normalize();
        
        // Recall this function after a time has passed
        randomTime = Random.Range(minimumDirectionTime, maximumDirectionTime);
        Invoke("ChangeDirection", randomTime);
    } 
}
