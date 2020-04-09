using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour {
	
	public static AsteroidGenerator instance;
	
    GameObject[] spawPositions;
    [HideInInspector]public List<GameObject>spawnedAsteroids;
	
	void Start () {

		if(instance==null)
		{
			instance = this;	
		}
		
        spawPositions = GameObject.FindGameObjectsWithTag("SpawPointAsteroid");

		spawnedAsteroids = new List<GameObject>();
		//PoolManager.
		
		for(int i = 0; i < spawPositions.GetLength(0); i ++)
		{
			PoolManager.Spawn("AsteroidPrimario").transform.position = spawPositions[i].transform.position;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu", 1f);
		}

	}
	
	public void Reposition(GameObject aste)
	{
		
	}
	
	public void OutOfWorld(GameObject obj)
	{
		obj.transform.position = spawPositions[Random.Range(0, spawPositions.GetLength(0))].transform.position;
		obj.GetComponent<Asteroid>().Init();
	}

}
