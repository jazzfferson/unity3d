using UnityEngine;
using System.Collections;

public class BodyImpactSound : MonoBehaviour {

    [SerializeField]
    private AudioSource lowCrash, highCrash;

    [SerializeField]
    private float highCrashThresholdSpeed = 40;

    private CarManager m_carManager;

    void Awake()
    {
        m_carManager = GetComponent<CarManager>();
    }


	void OnCollisionEnter(Collision other)
    {
        if(m_carManager.SpeedKmh<highCrashThresholdSpeed)
        {
            lowCrash.volume = m_carManager.SpeedKmh / highCrashThresholdSpeed;
            lowCrash.Play();
        }
        else
        {
            highCrash.Play();
        }
    }
}
