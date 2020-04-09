using UnityEngine;
using System.Collections;

public class CameraExploration : MonoBehaviour {

	public Transform target;
	public float damping;
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	

		transform.position = new Vector3(Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime * damping), transform.position.y,Mathf.Lerp(transform.position.z,target.position.z,Time.deltaTime* damping));
		
	}
}
