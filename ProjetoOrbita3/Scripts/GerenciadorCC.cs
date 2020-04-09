using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorCC : MonoBehaviour {

	private List<GameObject> targets;
	float forca;
    public float G ;
	float distancia;

	void Start()
	{
		targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("celestialBody"));
	}
	// Update is called once per frame
	void Update () {
		Gravi();
	}
	
	public void AddBody(GameObject body)
	{
		body.tag = "celestialBody";
		targets.Add(body);
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
						forca = G * (targets[i].GetComponent<Rigidbody>().mass*targets[j].GetComponent<Rigidbody>().mass / distancia*distancia);
						direcao = (targets[j].transform.position-targets[i].transform.position).normalized;
      					targets[i].GetComponent<Rigidbody>().AddForce(direcao*forca);
					}
				}
		}
   	}
}
