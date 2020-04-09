using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	
	private GameObject[] layers;
	
	void Start()
	{
		
		layers = GameObject.FindGameObjectsWithTag("BolinhaMenuBG");
		
		for(int i = 0; i <layers.Length; i++)
		{
			Go.to(layers[i].GetComponent<UISprite>(),3,new GoTweenConfig().colorProp("color",new Color(1,1,1,0.05f),false).setIterations(2000,GoLoopType.PingPong).setDelay(Random.Range(0.5f,4f)));
		}
	}
}
