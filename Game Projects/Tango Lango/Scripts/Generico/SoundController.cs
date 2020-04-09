using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{

    public static SoundController instance;
    public AudioSource audioSource;
    public AudioClip[] Sfxs;
    public AudioClip[] Musicas;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
            PlayMusic(0, 1);
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayMusic(int index, float volume)
    {
        audioSource.clip = Musicas[index];
        audioSource.Play();
    }
    public void PlaySfx(int index, float volume)
    {
		
		//print(index);
        gameObject.audio.PlayOneShot(Sfxs[index], volume);

    }
}
