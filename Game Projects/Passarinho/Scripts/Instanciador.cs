using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instanciador : MonoBehaviour {
	
	public static Instanciador instancia;
    [SerializeField]
	private GameObject[] gameObjects;
    [SerializeField]
    private AudioClip[] sounds;
    [SerializeField]
    private GameObject audioPlayOneShot;
   
	
	void Start()
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
    public GameObject Instanciar(int index, Vector3 posicao, Quaternion rotacao,float destroyTime)
    {
        GameObject obj = (GameObject)Instantiate(gameObjects[index], posicao, rotacao);
        Destroy(obj, destroyTime);
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
        GameObject inst = (GameObject)Instantiate(audioPlayOneShot, Vector3.zero, Quaternion.identity);
        inst.GetComponent<SoundOneShot>().PlaySfx(sounds[index], volume, pitch, delay);
    }
}
