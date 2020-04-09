using UnityEngine;
using System.Collections;

public class ContaGiro : MonoBehaviour {

    public UISprite valueSprite;
    public UILabel valueLabel;

    public float Rpm
    {
        get;
        set;

    }


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        valueLabel.text = ((int)Rpm).ToString();
        valueSprite.fillAmount = Mathf.Clamp(Rpm/20000, 0, 0.5f);
	}
}
