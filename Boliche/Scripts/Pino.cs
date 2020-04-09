using UnityEngine;
using System.Collections;

public class Pino : MonoBehaviour {
	
	
	public int ID;
	[HideInInspector] 
    public Vector3 posicaoOriginal;
    //[HideInInspector]
    public bool derrubado = false;
	
	void Start () {
		
		posicaoOriginal = gameObject.transform.position;
	}
	public bool DesligarFisica(bool ativo)
	{
		gameObject.GetComponent<Rigidbody>().isKinematic = ativo;
		return gameObject.GetComponent<Rigidbody>().isKinematic;
	}
	public void ResetarPino()
	{
		gameObject.transform.rotation = new Quaternion(0,0,0,0);
		gameObject.transform.position = new Vector3(posicaoOriginal.x,posicaoOriginal.y,posicaoOriginal.z);
	}

}
