using UnityEngine;
using System.Collections;

public class FindNearst : MonoBehaviour {

	
	
	[SerializeField]
	private float distanciaMinParaCalculo;
	private Vector3 posicaoDaMenorDistancia;
	private float menorDistancia = 999999;
	[HideInInspector] public GameObject target;
	
	// Update is called once per frame
	void Update () {
		
	 for(int i = 0; i< AsteroidGenerator.instance.spawnedAsteroids.Count;i++)
	 for(int j = 0; j< AsteroidGenerator.instance.spawnedAsteroids.Count;j++)
	  {
			
		if(menorDistancia>Vector3.Distance(transform.position,AsteroidGenerator.instance.spawnedAsteroids[j].transform.position) && i!=j)
		{
		   menorDistancia = Vector3.Distance(transform.position,AsteroidGenerator.instance.spawnedAsteroids[j].transform.position);
		   posicaoDaMenorDistancia = AsteroidGenerator.instance.spawnedAsteroids[j].transform.position;
		}
	  }
		
		

	
		
		menorDistancia = 999999;
    }
	

	
		
	

}
