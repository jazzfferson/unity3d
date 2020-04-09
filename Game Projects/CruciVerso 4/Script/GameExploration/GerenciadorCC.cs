using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorCC : MonoBehaviour {
	
	public static GerenciadorCC instance;
	private List<GameObject> targets;
	public float massa;
	float forca;
    public float G ;
	public float distanciaMinima;
	float distancia;

	void Start()
	{
		if(instance==null)
			instance = this;
		targets = new List<GameObject>();
		
	}

	void Update () {
		Gravi();
	}
	
	void Gravi() 

	{	
		Vector3 direcao;
		
		if(targets!=null)
		{
			
				for(int j = 0; j< targets.Count; j++)
				{

					 if(Vector3.Distance(targets[j].transform.position,transform.position)<distanciaMinima)
					{
						distancia = Vector3.Distance(targets[j].transform.position, transform.position);	
						forca = G * (targets[j].GetComponent<Rigidbody>().mass*massa / distancia*distancia);
						direcao = (targets[j].transform.position-transform.position).normalized;
      					targets[j].GetComponent<Rigidbody>().AddForce(direcao*forca);
					}
				}
		}
   	}
	
	public void AddBody(GameObject target)
	{
		targets.Add(target);
	}
	
	public void RemoveBody(GameObject target)
	{
		targets.Remove(target);
		targets.TrimExcess();
	}	
}
