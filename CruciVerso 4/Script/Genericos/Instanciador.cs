using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instanciador : MonoBehaviour {
	
	public static Instanciador instancia;
	public GameObject[] gameObjects;
	public AudioClip[] sounds;
	
	void Awake()
	{
		if(instancia==null)
		{
			instancia = this;
		}
	}
	
	public GameObject Instanciar (int index, Vector3 posicao, Quaternion rotacao)
	{
		return (GameObject)Instantiate(gameObjects[index],posicao,rotacao);
	}
	public void PlaySfx(int index,float volume)
	{
		GetComponent<AudioSource>().PlayOneShot(sounds[index],volume);
	}
}
