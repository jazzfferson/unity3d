using UnityEngine;
using System.Collections;

public class WheelAro : MonoBehaviour {

    public AnimationCurve curve;
    Wheel roda;
    public MeshRenderer aro;
    public float speed;

	void Start () {

        roda = GetComponent<Wheel>();
	}
	
	// Update is called once per frame
	void Update () {

        float alpha = curve.Evaluate(Mathf.Abs(roda.angularVelocity));
        aro.materials[1].color = new Color(1, 1, 1, alpha);
	}
}
