using UnityEngine;
using System.Collections;

public class FakeExplosion : MonoBehaviour {

	Vector3 randomDiretion;
	Vector3 randomRotation;
	public float velocidade;
	
	void Start () {
		
		transform.localScale = transform.localScale * Random.Range(1f,3f);
		Destroy(gameObject,4);
		randomDiretion = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));
	    randomRotation = new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),Random.Range(-5f,5f));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		transform.Rotate(randomRotation * Time.deltaTime * 30);
		transform.position+= randomDiretion * velocidade * Time.deltaTime;
		
	
	}
}
