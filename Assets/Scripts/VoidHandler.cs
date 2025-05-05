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

    // OnTriggerEnter gets called when another object enters the collision of this object
    private void OnTriggerEnter(Collider other)
    {
        // creates an empty variable to store the rigidbody physics object of the other
        Rigidbody otherRB;
        
        // checks the tag of the object that collided with the void
        switch(other.gameObject.tag) {
            case "Player":
                // if the object is the player, set the position of them to a specific fallback value
                other.gameObject.transform.position = fallback;
                otherRB = other.gameObject.GetComponent<Rigidbody>();
                otherRB.linearVelocity = Vector3.zero;  // set the velocity to zero so the player doesn't go flying down
                break;
            
            case "OtherPartyMember": // Watch out in case you change this tag name later-- don't forget to update this
                // if the object is another party member, reset their velocity and set their position to the designated main character
                other.gameObject.transform.position = mainCharacter.transform.position;
                otherRB = other.gameObject.GetComponent<Rigidbody>();
                otherRB.linearVelocity = Vector3.zero;
                break;
            
            case "NPC":
                // Need to figure out what to do in this case...
                break;
            
            default:
                // if none of the above cases applied, destroy the object
                Destroy(other.gameObject);
                break;
        }
    }
}
