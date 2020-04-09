using UnityEngine;
using System.Collections;

public class CameraThirdView : MonoBehaviour {

// The target we are following
public Transform target;
public Vector3 offsetPosition;
public float offsetRotationY;
public bool smooth = false;
// The distance in the x-z plane to the target
public float distance = 10.0f;
// the height we want the camera to be above the target
public float height = 5.0f;
// How much we 
public float heightDamping = 2.0f;
public float rotationDamping = 3.0f;


void FixedUpdate()
{

    // Early out if we don't have a target

    // Calculate the current rotation angles
    var wantedRotationAngle = target.eulerAngles.y + offsetRotationY;
    var wantedRotationAngleX = target.eulerAngles.x;

    var wantedHeight = target.position.y + height + offsetPosition.y;

    var currentRotationAngle = transform.eulerAngles.y;
    var currentRotationAngleX = transform.eulerAngles.x;

    var currentHeight = transform.position.y;

    if (smooth)
    {
        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.fixedDeltaTime);
        currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDamping * Time.fixedDeltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);
    }

    else
    {
        currentRotationAngle = wantedRotationAngle;
        currentRotationAngleX = wantedRotationAngleX;
        currentHeight = wantedHeight;

    }


    // Convert the angle into a rotation
    var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngle, 0);

    // Set the position of the camera on the x-z plane to:
    // distance meters behind the target
    transform.position = target.GetComponent<Rigidbody>().transform.position + offsetPosition;
    transform.position -= currentRotation * Vector3.forward * distance;

    // Set the height of the camera
    transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

    // Always look at the target
    transform.LookAt(target.GetComponent<Rigidbody>().transform.position + offsetPosition);
}
}
