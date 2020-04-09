using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gravidade : MonoBehaviour {

	// Use this for initialization
	public GameObject prefab;
	public float espacamento;
    public float tamanhoMinimo;
    public float tamanhoMaximo;
    public Vector3 randomRGB;
	public int quantidade;
    public List<GameObject> targets;
	float forca;
    public float G ;
	float distancia;
	
	void Start () {

       

		for(int i = 0; i < quantidade; i++)
		{
            float scale = Random.RandomRange((float)tamanhoMinimo, (float)tamanhoMaximo);
            GameObject obj = (GameObject)Instantiate(prefab, new Vector3(Random.Range(-espacamento, espacamento), Random.Range(-espacamento, espacamento), Random.Range(-espacamento, espacamento)), Quaternion.identity);
            obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, randomRGB.x), Random.Range(0f, randomRGB.y), Random.Range(0f, randomRGB.z), 1);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.GetComponent<Rigidbody>().mass = scale * 10;
			targets.Add(obj);
		}
	 	
		//Constante Real G 0.000000000667
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
	
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
						forca = G * (targets[i].GetComponent<Rigidbody>().mass*targets[j].GetComponent<Rigidbody>().mass / distancia*distancia);
						direcao = (targets[j].transform.position-targets[i].transform.position).normalized;
      					targets[i].GetComponent<Rigidbody>().AddForce(direcao*forca);
					}
				}
		}
   	}
 }	

