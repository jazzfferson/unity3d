using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {

	public UILabel label;
	public float time;
	public float position;
	public GoEaseType ease;

	public void Initialize (int valor) {

		label.text = "x" + valor;
		TweenAlpha.Begin (label.gameObject, time, 0);
		Go.to (this.transform, time, new GoTweenConfig ()
		       .position (new Vector3 (0, position, 0), true)
		       .setEaseType (ease)
		       .onComplete(completed=>Destroy(this.gameObject)));

	}
	
}
