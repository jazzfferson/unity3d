using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	// the circle object to be instantiated
	public GameObject greenCirclePrefab; //used to instanciate a new green circle
	// some qty circle controls 
	public int maxCircle = 10;
	public int count = 0;

	// Use this for initialisation
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// instanciate a new circle
		if (count < maxCircle){ // maximum 10 circles same time
			Instantiate(greenCirclePrefab, transform.position, transform.rotation);
			count++;
		}	
	}
}
