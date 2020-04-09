using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	// Use this for initialization
	
	[HideInInspector] public float velocidade = 1f;
	[HideInInspector] public float tempoVida = 2.0f;
	[HideInInspector] public float precisao = 3.0f;
	
	public GameObject explosaoLaser;
	
	
	void Start () {
	
		Destroy(gameObject,Random.Range(tempoVida/2,tempoVida));
		transform.Rotate(new Vector3(Random.Range(0,precisao),Random.Range(0,precisao),Random.Range(0,precisao)));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		transform.Translate(Vector3.forward * (Time.deltaTime * velocidade));
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		/*Debug.Log("Colidiu");
		Instantiate(explosaoLaser,gameObject.transform.position,gameObject.transform.rotation);
		Destroy(gameObject);*/
	}
}
