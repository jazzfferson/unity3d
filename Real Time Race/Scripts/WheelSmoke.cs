using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Wheel))]
public class WheelSmoke : MonoBehaviour {


    const int maxEmissionRate=30;
    const float minSlipTrack = 20, minSlipGround = 5f, multiplyEmissionRate = 0f;
    private ParticleSystem _smoke, _dirt;
    private Wheel wheel;

    void Start()
    {
        if (StaticReferences.Instance == null)
            return;
        _smoke = (ParticleSystem)Instantiate(StaticReferences.Instance.trackSmoke, transform.position, Quaternion.identity);
        _dirt = (ParticleSystem)Instantiate(StaticReferences.Instance.offroadSmoke, transform.position, Quaternion.identity);
        _smoke.transform.parent = transform;
        _dirt.transform.parent = transform;
        wheel = GetComponent<Wheel>();
    }
	void Update () {

        if (StaticReferences.Instance == null)
            return;

            float slip = wheel.slipVelo;
            float wheelSpeed = wheel.angularVelocity;

            if (slip > minSlipTrack && wheel.surfaceType == CarDynamics.SurfaceType.track)
            {
                    _smoke.emissionRate = Mathf.Clamp(slip * multiplyEmissionRate, 0, maxEmissionRate);
            }
            else
            {
                _smoke.emissionRate = 0;
            }

            if (wheelSpeed>12 && wheel.surfaceType == CarDynamics.SurfaceType.offroad)
            {
                _dirt.emissionRate = Mathf.Clamp(((wheelSpeed / 12) * multiplyEmissionRate) + slip * multiplyEmissionRate, 0, maxEmissionRate);
            }
            else
            {
                _dirt.emissionRate = 0;
            }
	}
}
