using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkText : MonoBehaviour {

    private Text text;
    [SerializeField]
    private float speed;
    [SerializeField]
    private AnimationCurve alphaCurve;
    private float alpha = 1;
    public bool disable = false;

    public Text TextComponent
    {
        get { return text; }
    }

	void Awake () {
        text = GetComponent<Text>();
    }
	
	
	void Update () {

        alpha = alphaCurve.Evaluate(Mathf.PingPong(Time.time * speed, 1));
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
