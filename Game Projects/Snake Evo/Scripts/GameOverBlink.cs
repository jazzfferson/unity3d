using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverBlink : MonoBehaviour {

    public float timer = 0.5f;
    float _timer;

    public Text text;

	void Start () {
        _timer = timer;
	}
	
	// Update is called once per frame
	void Update () {

        _timer -= Time.deltaTime;
	
        if(_timer <=0)
        {
            _timer = timer;
            text.enabled = !text.enabled;
        }
	}
}
