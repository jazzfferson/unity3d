using UnityEngine;
using System.Collections;

public class NaveIA : MonoBehaviour {

	public Transform alvo;
	public float velocidade;
	public float distanciaMinima;
	public float proximidadeMaxima;
	
	void Start () {

	}

	void Update () {
		
		if(Vector3.Distance(transform.position,alvo.position)<distanciaMinima)
		{
			Perseguir();
		}
		
		GetComponent<Metralhadora>().PararGerar();
	
	}
	
	void Perseguir()
	{
		transform.LookAt(alvo);
		transform.Translate(Vector3.forward * Time.deltaTime* velocidade);
	}
	void AndarNormalmente()
	{
		
	}
}
