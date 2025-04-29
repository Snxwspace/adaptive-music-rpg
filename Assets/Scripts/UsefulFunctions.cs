using Unity.Mathematics;
using UnityEngine;

public class UsefulFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGrounded(float sensitivity, float width) {
        if (Physics.SphereCast(transform.position, width, Vector3.down, out _, sensitivity)) {
            return true;
        }
        return false;
    }

    public Vector3 NormalizeMoveVector(Vector3 direction) {
        float xFactor = 1.0f;
        float zFactor = 1.0f;
        if(direction.x != 0) {
            xFactor = math.abs(direction.x);
        }
        if(direction.z != 0) {
            zFactor = math.abs(direction.z);
        }
        direction.x /= xFactor;
        direction.z /= zFactor;
        direction.Normalize();
        direction.x *= xFactor;
        direction.z *= zFactor;
        return direction;
    }
}


// so basically in order to actually get this to function, i'll have to create a new member of the class
    // then use that to call any methods inside of it, because you can't call any methods
    // from a class itself, you have to be calling a method of an object, just keep this in mind for the future
