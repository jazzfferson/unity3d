using UnityEngine;
using System.Collections;

public class GradienteLoopColor : MonoBehaviour {

    public Gradient gradient;
    public float speed;
    private Color _colorResult;
    public Color colorResult
    {
        get { return _colorResult; }
    }



	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

        float time = Mathf.PingPong(Time.time * speed, 1);
        _colorResult = gradient.Evaluate(time);
	}
}
