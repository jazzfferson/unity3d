using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 0.5f;
	Vector3 targetPosition;
	Vector3 vel;


	// Update is called once per frame
	void Update () {

	
		targetPosition = new Vector3 (target.position.x, transform.position.y, target.position.z);
		Vector3 lerpPosition = Vector3.SmoothDamp (transform.position, targetPosition, ref vel, smoothSpeed);
		transform.position = lerpPosition;

	}
}
