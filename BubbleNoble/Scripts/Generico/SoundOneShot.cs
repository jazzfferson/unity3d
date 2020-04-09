using UnityEngine;
using System.Collections;

public class SoundOneShot : MonoBehaviour {

	public void PlaySfx(AudioClip audio,float volume,float pitch,float delay)
	{
		GetComponent<AudioSource>().clip = audio;
		GetComponent<AudioSource>().pitch = pitch;
		GetComponent<AudioSource>().volume = volume;
		GetComponent<AudioSource>().PlayDelayed(delay);
		Destroy(gameObject, audio.length + delay);
	}
}
