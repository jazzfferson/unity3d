using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instanciador : MonoBehaviour {
	
	public static Instanciador instancia;
	public GameObject[] gameObjects;
	public AudioClip[] sounds;
	public GameObject audioPlayOneShot;
	
	void Awake()
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

	
	public GameObject Instanciar(int index, Vector3 posicao, Quaternion rotacao)
	{
		return (GameObject)Instantiate (gameObjects [index], posicao, rotacao);
	}
    public GameObject Instanciar(int index, Vector3 posicao, Quaternion rotacao,Transform parent)
    {
        GameObject obj = (GameObject)Instantiate(gameObjects[index], posicao, rotacao);
        obj.transform.parent = parent;
        return obj;
    }
    public GameObject InstanciarLocal(int index, Vector3 Localposicao, Quaternion rotacao, Transform parent)
    {
        GameObject obj = (GameObject)Instantiate(gameObjects[index], Localposicao, rotacao);
        obj.transform.localPosition = Localposicao;
        obj.transform.parent = parent;
        return obj;
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
        if (Proprietes.MuteAudio)
            return;
        GameObject inst = (GameObject)Instantiate(audioPlayOneShot, Vector3.zero, Quaternion.identity);
        inst.GetComponent<SoundOneShot>().PlaySfx(sounds[index], volume, pitch, delay);
    }
}
