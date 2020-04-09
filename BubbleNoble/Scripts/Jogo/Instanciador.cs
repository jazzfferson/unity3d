using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instanciador : MonoBehaviour {
	
	public static Instanciador instancia;
	public GameObject[] gameObjects;
	public AudioClip[] sounds;
	public GameObject audioPlayOneShot;
	
	void Start()
	{
		
		if(instancia==null)
		{
			DontDestroyOnLoad(gameObject);
			instancia = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	
	public GameObject Instanciar (int index, Vector3 posicao, Quaternion rotacao)
	{
		return (GameObject)Instantiate(gameObjects[index],posicao,rotacao);
	}

	public void PlaySfx(int index,float volume,float pitch)
	{
        play(index, volume, pitch, 0);
	}
	public void PlaySfx(int index,float volume,float pitch,float delay)
	{
        play(index, volume, pitch, delay);
	}

    void play(int index, float volume, float pitch, float delay)
    {
        if (Proprietes.muteSfx)
            return;
        GameObject inst = (GameObject)Instantiate(audioPlayOneShot, Vector3.zero, Quaternion.identity);
        inst.GetComponent<SoundOneShot>().PlaySfx(sounds[index], volume, pitch, delay);
    }
}
