using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorCC : MonoBehaviour {
	
	public List<GameObject> CorposCelestes;
	
	void Start () {
		
		for(int i = 0; i<CorposCelestes.Count; i++)

			CorposCelestes[i].GetComponent<Gravidade>().targets = CorposCelestes;

	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
}
