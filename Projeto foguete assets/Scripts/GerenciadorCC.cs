using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorCC : MonoBehaviour {
	
	public static GerenciadorCC instance;
	private List<GameObject> targets;
	float forca;
    public float G ;
	float distancia;

	void Start()
	{
		if(instance==null)
			instance = this;
		
		targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("celestialBody"));
		
	}

	void Update () {
		Gravi();
	}
	
	void Gravi() 

	{	
		Vector3 direcao;
		
		if(targets!=null)
		{
			for(int i = 0; i< targets.Count; i++)
				for(int j = 0; j< targets.Count; j++)
				{
					
					if(i==j)
					{
					
					}
					else
					{
						distancia = Vector3.Distance(targets[i].transform.position, targets[j].transform.position);	
						forca = G * (targets[i].rigidbody.mass*targets[j].rigidbody.mass / distancia*distancia);
						direcao = (targets[j].transform.position-targets[i].transform.position).normalized;
      					targets[i].rigidbody.AddForce(direcao*forca);
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
