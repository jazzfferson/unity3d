using UnityEngine;
using System.Collections;

public class ColorByVelocity : MonoBehaviour {

    public float divideBy=1;
    
	// Update is called once per frame
	void Update () {

        float mag = GetComponent<Rigidbody>().velocity.magnitude / divideBy;
        gameObject.GetComponent<Renderer>().material.color = new Color(0.01f + mag, 0.01f + mag, 0.01f + mag);
	}
}
