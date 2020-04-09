using UnityEngine;
using System.Collections;
public enum CenaJogo{Intro,Menu,Jogo,Loading};
public class MainGame : MonoBehaviour {

	
	public FadeScreen fadeScreen;
	public Loading loading;
	
	void Awake () {
		
    	DontDestroyOnLoad (gameObject.transform);				
	}
	void Start()
	{
		StartCoroutine(Introducao());
		
	}


	IEnumerator Introducao()
	{
		yield return new WaitForSeconds(2f);
		fadeScreen.FadeToBlack(1);
		yield return new WaitForSeconds(1.1f);
		fadeScreen.logo.SetActive(false);
		fadeScreen.FadeToTransparent(0.01f);
		loading.LoadScene("Menu");

	}
	
}
