using UnityEngine;
using System.Collections;

public class AsteroidMenu : MonoBehaviour {

	Vector3 randomRotation;
	float randomVelocityPosition = 6f;
	public float velocity = 2f;
	
	
	void Start () {
		
		randomRotation  = new Vector3(Random.Range(-randomVelocityPosition,randomVelocityPosition),0,Random.Range(-randomVelocityPosition,randomVelocityPosition));
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(randomRotation * Time.deltaTime * velocity);
		
	
	}
}
