using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private Vector3 direction;
    public float minimumDirectionTime = 4.0f;
    public float maximumDirectionTime = 7.0f;
    private float randomTime;
    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // starts the chain of the object changing directions randomly
        Invoke("ChangeDirection", 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    void ChangeDirection() {
        direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        direction.Normalize();  // this is okay instead of using my designated function for it because it strictly moves at max speed, there's no velocity data that's lost from doing this
        
        // Recall this function after a time has passed
        randomTime = Random.Range(minimumDirectionTime, maximumDirectionTime);
        Invoke("ChangeDirection", randomTime);
    } 
}
