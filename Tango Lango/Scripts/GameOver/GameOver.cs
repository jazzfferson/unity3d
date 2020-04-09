using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
    public UISprite materialGameOver;
    public UISprite materialNewRecorde;
    float contador;
	bool next = true;

	void Start () 
    {
	    //checar se foi recorde
        if (GameData.ultimaPontucao == GameData.GetRecorde())
        {
            materialNewRecorde.gameObject.SetActive(true);
        }
        else
        {
            materialGameOver.gameObject.SetActive(true);
        }

        contador = 2;
	}
	

	void Update () 
    {
        contador -= Time.deltaTime;
        if (contador <= 0 && next)
        {
           // Application.LoadLevel("Menu");
			LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu", 0.5f);
			next = false;
        }
	}
}
