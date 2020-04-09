using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	
	[SerializeField]
	private float maxRotation;
	private Vector3 rotation;
	
	void Start () {
		
		rotation = new Vector3(Random.Range(-maxRotation,maxRotation),Random.Range(-maxRotation,maxRotation),Random.Range(-maxRotation,maxRotation));
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(rotation * Time.deltaTime);
		
	}
}
