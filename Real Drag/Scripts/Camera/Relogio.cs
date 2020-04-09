using UnityEngine;
using System.Collections;

public class Relogio : MonoBehaviour {

	public Transform pivo;
	public float offsetRotation;
	public float scalar;
	

	
	public void UpdatePivo (float amount) {
		
		
		pivo.transform.localEulerAngles = new Vector3(pivo.transform.rotation.x,pivo.transform.rotation.y,offsetRotation + amount * scalar);
	
	}

}
