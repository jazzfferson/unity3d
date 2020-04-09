using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeradorCometas : MonoBehaviour {
	[SerializeField]
	private GameObject prefab;
	[SerializeField]
	private Transform[]spawPoints;
	[SerializeField]
	private float intervaloMinOnda,intervaloMaxOnda,intervaloMinInstanciar,intervaloMaxInstanciar,distanciaParaDestruirCometas;
	
	private List<GameObject> instancias;
	
	void Start () {
	
		
		instancias = new List<GameObject>();

		StartCoroutine(rotina());
	}
	
	// Update is called once per frame
	void Update () {
	
		for(int i = 0; i < instancias.Count;i++)
		{
			if(Vector2.Distance(instancias[i].transform.position,Camera.main.transform.position)>distanciaParaDestruirCometas)
			{
				Destroy(instancias[i]);
				instancias.RemoveAt(i);
				instancias.TrimExcess();
			}
		}
		
	}
	
	IEnumerator rotina()
	{
		
		for(int i=0;i<spawPoints.Length;i++)
		{
			yield return new WaitForSeconds(Random.Range(intervaloMinInstanciar,intervaloMaxInstanciar));
			instancias.Add((GameObject)Instantiate(prefab,spawPoints[i].position,Quaternion.identity));
		}
		
		yield return new WaitForSeconds(Random.Range(intervaloMinOnda,intervaloMaxOnda));
		StartCoroutine(rotina());
		
	}
}
