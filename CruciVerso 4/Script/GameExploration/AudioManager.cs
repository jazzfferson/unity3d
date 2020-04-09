using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance;
	
	public List<AudioSource> laserAudioSource;
	int laserIndice;
	
	public List<AudioSource> impactAudioSource;
	int impactIndice;
	
	public List<AudioSource> explosionAudioSource;
	int explosionIndice;
	
	
	void Start()
	{
		if(instance ==null)
		{
			instance = this;
		}
	}
	
	public void PlayLaser()
	{
		
		laserAudioSource[laserIndice].Play();
		laserIndice++;
		if(laserIndice>=laserAudioSource.Count)
		{
			laserIndice = 0;
		}
	}
	
	public void PlayImpact()
	{
		
		impactAudioSource[impactIndice].Play();
		impactIndice++;
		if(impactIndice>=impactAudioSource.Count)
		{
			impactIndice = 0;
		}
	}
	
	public void PlayExplosion()
	{
		
		explosionAudioSource[explosionIndice].Play();
		explosionIndice++;
		if(explosionIndice>=explosionAudioSource.Count)
		{
			explosionIndice = 0;
		}
	}
}
