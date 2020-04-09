using UnityEngine;
using System.Collections;

public class MoveInDirection : MonoBehaviour {

	
	public Vector3 direction;
	public float minSpeed,MaxSpeed;
	private float speed;
	void Start () {
		
		speed = Random.Range(minSpeed,MaxSpeed);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
	transform.Translate(direction*(Time.deltaTime*speed)/10);
	
	}
}
