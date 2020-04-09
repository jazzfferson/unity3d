using UnityEngine;
using System.Collections;

public class Gravidade : MonoBehaviour {

	// Use this for initialization
	public GameObject target;
	float forca;
    float G ;
	float massa1;
	float massa2;
	float distancia;
	
	void Start () {
	 	
		//Constante Real G 0.000000000667
	    G = 0.000000000667f;
		
		massa1 = gameObject.rigidbody.mass;
		massa2 = target.gameObject.rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
		
		Gravi();
	
	}
	
	
	void Gravi() 

	{	
		if(target!=null)
		{
			distancia = Vector3.Distance(target.transform.position, gameObject.transform.position);	
			forca = G * (massa1*massa2 / distancia*distancia);	
      		target.rigidbody.velocity += (transform.position - target.transform.position).normalized * forca * Time.deltaTime; 
		}
   	}
 }	

