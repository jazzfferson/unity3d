using UnityEngine;
using System.Collections;

public class LancaMissil : MonoBehaviour {
	
	[SerializeField]
	private float velocidadeMissil;
	[SerializeField]
	private GameObject missil;
	[SerializeField]
	private Transform[] missilPosition;

	private void Launch(int id)
	{
		GameObject obj = PoolManager.Spawn("Missil");
		if(obj==null)
			return;
		obj.transform.position = missilPosition[id].position;
		obj.transform.rotation = transform.rotation;
		obj.GetComponent<Missil>().Init(velocidadeMissil,GetComponent<Rigidbody>().velocity.magnitude);
	}
	
}
