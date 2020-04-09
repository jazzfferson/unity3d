using UnityEngine;
using System.Collections;

public class Nave : MonoBehaviour {

    public float acceleration,speed;
	void Start () {
	
	}
	
	
	void Update () {

        float lado = Input.GetAxis("Horizontal");
        float frente = Input.GetAxis("Vertical");
        float deltaTime = Time.deltaTime * acceleration;
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(lado, 0, frente) * speed,deltaTime);


	}
}
