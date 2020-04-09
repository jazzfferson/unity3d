using UnityEngine;
using System.Collections;

public class Bola : MonoBehaviour {

	void OnCollisionEnter(Collision collider)
	{
		if(collider.gameObject.CompareTag("Canaleta"))
		{
			OnJogadaInvalida();
		}
	}
	void OnCollisionStay(Collision collider)
	{
		if(collider.gameObject.CompareTag("Canaleta"))
		{
			gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,120));
		}
	}
	void OnJogadaInvalida() 
	{
                if (JogadaInvalida != null)
                JogadaInvalida(this.gameObject);
	}
	
	void Update()
	{
		if(gameObject.transform.position.z>=180)
		{
			Destroy(gameObject,5);
		}
	}
	public delegate void EventHandler(GameObject e);
	public event EventHandler JogadaInvalida;
}
