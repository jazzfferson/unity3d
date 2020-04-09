using UnityEngine;
using System.Collections;

public class CopyPosRot : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public Vector3 offsetRot;
	
	void Update () {

        transform.position = target.position + offset;
        transform.eulerAngles = target.eulerAngles + offsetRot;
	
	}
}
