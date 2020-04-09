using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {
	
	public static Loading instance;
	
	public GameObject loadingIcon;
	public GameObject telaLoading;
	public bool loadScreen = false;
	
	void Start()
	{
		if(instance==null)
		{
			instance=this;
		}
		
	}
	
	void Update()
	{
		if(loadScreen)
		{
			loadingIcon.transform.Rotate(0,0,50*Time.deltaTime);
			loadingIcon.SetActive(loadScreen);
			telaLoading.SetActive(loadScreen);
		}
		
	}
}