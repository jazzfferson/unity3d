using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UnityUIButtonSound: UnityUIButtonClickFrequencyLimiter
{
    protected AudioSource m_AudioSource;
    public AudioSource AudioSounce { get => m_AudioSource;}
    protected override void Awake()
    {
        base.Awake();
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        button.onClick.AddListener(PlaySound);
       
    }

    protected virtual void PlaySound()
    { 
        m_AudioSource.Play();
    }
}