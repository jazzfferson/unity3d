using UnityEngine;
using System.Collections;

public class BezierMover : MonoBehaviour {

	public BezierCurve path;
	public AnimationCurve animationCurve;
	public float speed = 1;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float t = animationCurve.Evaluate(Mathf.PingPong(Time.time * speed,1));
		transform.position = path.GetPointAt(t);
		var rotation = Quaternion.LookRotation(transform.position, path.GetPointAt(t+0.01f));
		transform.rotation = rotation;
	
	}
}
