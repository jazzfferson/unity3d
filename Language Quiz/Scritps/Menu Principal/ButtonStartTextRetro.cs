using UnityEngine;
using System.Collections;

public class ButtonStartTextRetro : MonoBehaviour
{
    public static ButtonStartTextRetro instance;
	public UILabel texto;
    [SerializeField] float speed;
    public float Speed
    {
        get;
        set;
    }
    float time;

	private bool finished;
	private float alphaTarget;

    void Start()
    {
        if (instance == null) {instance = this;}
        Speed = speed;
    }
	void Update () {

       
          TextAnimationStandard();
	}

	void TextAnimationStandard()
	{
        if (time >= 1) 
		{
			alphaTarget = -0.1f;
            texto.alpha = 0;
		}
        else if (time <= 0)
		{
			alphaTarget = 1.1f;
            texto.alpha = 1;
		}
        time = Mathf.Lerp(time, alphaTarget, Time.deltaTime * Speed);
	}
}
