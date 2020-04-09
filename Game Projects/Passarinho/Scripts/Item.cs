using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public float speed,scaleSpeed,scaleAmount;
    public Transform hL1;
    Vector3 scale;
    void Start()
    {
        scale = hL1.transform.localScale;
    }
	// Update is called once per frame
	void Update () {

        hL1.transform.Rotate(0, 0,  speed * Time.deltaTime);
        hL1.transform.localScale = scale * (1 + Mathf.PingPong(Time.time * scaleSpeed, scaleAmount));
	
	}
}
