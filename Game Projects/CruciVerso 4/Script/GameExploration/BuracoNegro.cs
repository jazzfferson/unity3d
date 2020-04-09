using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuracoNegro : MonoBehaviour {
	
	
	List<GameObject> objs;
	
	void Start () {
		
		objs = new List<GameObject>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Force();
	
	}
    void OnTriggerEnter(Collider other)
    {
 		objs.Add(other.gameObject);
    }
	void Force()
	{
		Vector3 direcao; 
		
		for(int i = 0; i<objs.Count; i++)
		{
			direcao = transform.position - objs[i].transform.position;	
			objs[i].GetComponent<Rigidbody>().AddForce(direcao);
		}
	}
	void Remove(GameObject obj)
	{
		
	}
}
