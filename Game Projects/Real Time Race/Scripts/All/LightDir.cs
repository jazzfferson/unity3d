using UnityEngine;
using System.Collections;

public class LightDir : MonoBehaviour {

    public Vector4 position;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Shader.SetGlobalVector("_WorldSpaceLightPos0", position);
	
	}
}
