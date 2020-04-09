using UnityEngine;
using System.Collections;

public class SteeringHud : MonoBehaviour {

    public Transform pivoSteering;
    public float multiplicador;

	void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateSteering (float valor) {

        pivoSteering.localRotation = Quaternion.Euler(new Vector3(0, 0, valor * multiplicador));
	
	}
}
