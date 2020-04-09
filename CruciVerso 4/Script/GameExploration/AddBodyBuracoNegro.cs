using UnityEngine;
using System.Collections;

public class AddBodyBuracoNegro : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		GerenciadorCC.instance.AddBody(other.gameObject);
	}
}
