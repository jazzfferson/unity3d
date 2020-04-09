using UnityEngine;
using System.Collections;
public enum TipoColisao{Enter,Exit,Stay};
public class InstanciateObject : MonoBehaviour {

	
	public GameObject Obj;
	public TipoColisao tipoColisao;
	public bool selfPosition;
	public Vector3 worldPosition;
	public float delay;
	public string tagNameOtherCollider;
	
	[HideInInspector]public GameObject instatiatedObject;
	
	void OnCollisionEnter(Collision other)
	{
		if(tipoColisao==TipoColisao.Enter)
		{
			Colisao(other);
		}
	}
	void OnCollisionExit(Collision other)
	{
		if(tipoColisao==TipoColisao.Exit)
		{
			Colisao(other);
		}
	}
	void OnCollisionStay(Collision other)
	{
		if(tipoColisao==TipoColisao.Stay)
		{
			Colisao(other);
		}
	}
	
	void Colisao(Collision other)
	{
		if(other.gameObject.tag == tagNameOtherCollider && other.gameObject.tag!="")
		StartCoroutine(rotina(delay,other));
	}
	
	IEnumerator rotina(float delayRotina,Collision otherColisor)
	{
		yield return new WaitForSeconds(delayRotina);
		Instantiate(transform.position);
	}
	
	public GameObject Instantiate(Vector3 position)
	{
		if(selfPosition)
			instatiatedObject = (GameObject)Instantiate(Obj,position,Quaternion.identity);
		else
			instatiatedObject = (GameObject)Instantiate(Obj,worldPosition,Quaternion.identity);
		
		return instatiatedObject;
	}
	
}
