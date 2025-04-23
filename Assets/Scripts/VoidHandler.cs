using UnityEngine;

public class VoidHandler : MonoBehaviour
{
    public Vector3 fallback = new Vector3(0, 10, 0);
    public GameObject mainCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRB;
        switch(other.gameObject.tag) {
            case "Player":
                other.gameObject.transform.position = fallback;
                otherRB = other.gameObject.GetComponent<Rigidbody>();
                otherRB.linearVelocity = Vector3.zero;
                break;
            
            case "OtherPartyMember": // Watch out in case you change this later-- don't forget to update this
                other.gameObject.transform.position = mainCharacter.transform.position;
                otherRB = other.gameObject.GetComponent<Rigidbody>();
                otherRB.linearVelocity = Vector3.zero;
                break;
            
            case "NPC":
                // Need to figure out what to do in this case...
                break;
            
            default:
                Destroy(other.gameObject);
                break;
        }
    }
}
