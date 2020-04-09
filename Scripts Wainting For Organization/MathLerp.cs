using UnityEngine;
using System.Collections;

public class MathLerp : MonoBehaviour {

	[SerializeField] Vector3 direction = Vector3.left;
	[SerializeField] float distance = 5f;
	[SerializeField] float speed = 1f;
	[SerializeField] float duration = 1f;
	[SerializeField] AnimationCurve movementCurve = AnimationCurve.EaseInOut(0,0,1,1);

	private Vector3 startPosition;
	private float time;
	private Transform myTransform;

	void Start () {
	
		myTransform = GetComponent<Transform> ();
		startPosition = myTransform.position;

	}
	
	// Update is called once per frame
	void Update () {


		time += Time.deltaTime;
		myTransform.position = Vector3.Lerp (startPosition, startPosition + direction * distance, movementCurve.Evaluate (Mathf.PingPong (time * speed, duration)));
	
	}
}
