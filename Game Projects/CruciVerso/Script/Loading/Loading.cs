using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public GameObject loadingIcon;
	public UISprite[] telasLoading;
	
	float loadTime;
	float toleranceTime;
	bool loadScreen;
	
	void Start()
	{
		toleranceTime = 1;
	}
	
	public void LoadScene(string name)
	{
		Application.LoadLevelAsync(name);
	}
	
	void Update()
	{
		LoadingLevel();
	}
	
	void LoadingLevel()
	{
		
		if(Application.isLoadingLevel)
		{	
			loadTime+=Time.deltaTime;
			
			if(loadTime>=toleranceTime)
			{
				loadScreen = true;
			}
		}
		if(loadScreen)
		{
			RotateIcon(200);
			telasLoading[0].gameObject.SetActive(true);
			loadingIcon.SetActive(true);
		}
		else
		{
			telasLoading[0].gameObject.SetActive(false);
			loadingIcon.SetActive(false);
		}
	}
	
	void RotateIcon(float speed)
	{
		loadingIcon.transform.Rotate(0,0,speed*Time.deltaTime);
	}
	
	void OnLevelWasLoaded () {
		
		loadScreen= false;
	}
}