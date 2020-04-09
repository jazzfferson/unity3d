using UnityEngine;
using System.Collections;

public class ScreenAdjust : MonoBehaviour {

    public bool adjustWith, adjustHeigh;

	void Start () {

        UI2DSprite sprite = GetComponent<UI2DSprite>();

        if (adjustWith) {sprite.width = Screen.width;}
        if (adjustHeigh) {sprite.height = Screen.height;}
        
        
	}
	
	
}
