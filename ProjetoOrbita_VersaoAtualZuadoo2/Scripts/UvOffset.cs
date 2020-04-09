using UnityEngine;
using System.Collections;

public class UvOffset : MonoBehaviour {

	// Use this for initialization

    public Vector2 pos;

	void Start () {

        renderer.material.SetTextureOffset("_MainTex", new Vector2(pos.x, pos.y));
	
	}
	
	// Update is called once per frame
	void Update () {

        renderer.material.SetTextureOffset("_MainTex", new Vector2(pos.x, pos.y));
	
	}
}
