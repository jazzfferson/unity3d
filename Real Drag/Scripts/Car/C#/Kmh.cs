using UnityEngine;
using System.Collections;

public class Kmh : MonoBehaviour {

    public UISprite valueSprite;
    public UILabel valueLabel;
    public float divisor = 600f;
    public float KmhValue
    {
        get;
        set;
    }


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        valueLabel.text = KmhValue.ToString();
        valueSprite.fillAmount = KmhValue / divisor;

	}
}
