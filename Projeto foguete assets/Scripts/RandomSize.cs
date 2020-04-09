using UnityEngine;
using System.Collections;

public class RandomSize : MonoBehaviour {

	[SerializeField]
	private float minSize,maxSize;
	
	void Start () {
		transform.localScale = new Vector3(Random.Range(minSize,maxSize),Random.Range(minSize,maxSize),Random.Range(minSize,maxSize));
	}

}
