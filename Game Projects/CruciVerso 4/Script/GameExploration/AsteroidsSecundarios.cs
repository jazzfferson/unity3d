using UnityEngine;
using System.Collections;

public class AsteroidsSecundarios : MonoBehaviour {
	
	[SerializeField]
	private Rigidbody[] asteroids;
	[SerializeField]
	private float forceExplosion;
	[SerializeField]
	private float radiusExplosion;
	

	void Start()
	{
		foreach(Rigidbody body in asteroids)
		{
			body.AddExplosionForce(forceExplosion,transform.position,radiusExplosion);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
