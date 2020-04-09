using UnityEngine;
using System.Collections;

public class UvOffset : MonoBehaviour {

	// Use this for initialization

    public Vector2 pos;

	void Start () {

        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(pos.x, pos.y));
	
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(pos.x, pos.y));
	
	}
}
