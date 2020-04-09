using UnityEngine;
using System.Collections;

public class BackGroundOffset : MonoBehaviour {

	public UvOffset uvOffset;
	public float smooth;
	
	
	void Update () {
	
		uvOffset.pos = new Vector2(transform.position.x,transform.position.y) / smooth;
		
	}
}
