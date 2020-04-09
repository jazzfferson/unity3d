using UnityEngine;
using System.Collections;

public class ButtonExit : MonoBehaviour {
	
	
	public OratorMessage oratorExit;
	bool pressed;

	
	void Update()
	{
		
		
		if(Proprietes.estadoMenu!=EstadoMenu.Inicial)
			return;
		
		if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote)&& !ButtonInfo.info) && !pressed)
		{
			Proprietes.canClick = false;
			pressed = true;
			Pressed();

		}
		
		else if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote)&& !ButtonInfo.info) && pressed)
		{
			pressed = false;
			Proprietes.canClick = true;
			Invoke("DelayClick",1.4f);
			No();
		}
		
	}	
	void Pressed()
	{

		ButtonOptions.instance.Close();
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		oratorExit.ExibeMessage("");
	}
	void No()
	{	
		Instanciador.instancia.PlaySfx(3,0.4f,1);

		oratorExit.HideMessage();
	}
	void Yes()
	{
		#if UNITY_ANDROID
		AdvertisementHandler.DisableAds();
		#endif
		
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		Application.Quit();
	}

}
