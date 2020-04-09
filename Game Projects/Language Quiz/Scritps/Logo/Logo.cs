using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

    public UI2DSprite logo,loading;

	IEnumerator Start () {

        yield return new WaitForSeconds(2);
        TweenAlpha.Begin(logo.gameObject, 0.2f, 0);
        TweenAlpha.Begin(loading.gameObject, 0.2f, 1).delay = 0.2f;
        yield return new WaitForSeconds(0.4f);
        Application.LoadLevel(1);
	
	}
	
	// Update is called once per frame
	void Update () {


   
	}
}
