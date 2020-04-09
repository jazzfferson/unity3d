using UnityEngine;
using System.Collections;

public class Faps : MonoBehaviour {


    UI2DSprite sprite;
    public float rand;
    void Start()
    {
        sprite = GetComponent<UI2DSprite>();
    }
	void Update () {


        sprite.alpha = Mathf.PingPong(rand + Time.time * 4 * rand, 1);
	
	}
}
