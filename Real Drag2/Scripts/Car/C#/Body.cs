using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
	
	public UILabel kilometrosHora;
	public Transform cg;
	public float mass;
	public float atenuation;
	Vector3 lastVelocity;
	Vector3 acceleration;
	Vector3 cgPosition;
	
	void Start () {
		
		rigidbody.centerOfMass = cg.transform.localPosition;
		cgPosition = rigidbody.centerOfMass;
		rigidbody.mass = mass;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		 KimolemersPerHour();
		 
	}
	void FixedUpdate()
	{
		CenterOfMass();
	}
	void KimolemersPerHour()
	{
		kilometrosHora.text = Mathf.Clamp((int)Mathf.Abs(rigidbody.velocity.magnitude * 3.6f),0,10000).ToString();
	}
	void CenterOfMass()
	{
		acceleration = (rigidbody.velocity - lastVelocity) / Time.fixedDeltaTime;
		lastVelocity = rigidbody.velocity;
		rigidbody.centerOfMass = cgPosition - (acceleration/200);
		cg.localPosition = rigidbody.centerOfMass;
		
	}
}
