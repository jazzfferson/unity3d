using UnityEngine;
using System.Collections;

public class TrocaBolinha : MonoBehaviour {

	public UISprite[] bolinhas;
	public Color corBolinhaApagada;
	public Color corBolinhaAcesa;

	public void SetBolinha(int indice)
	{
		for(int i = 0; i < bolinhas.GetLength(0); i ++)
		{
			if(i == indice)
			{
					bolinhas[i].color = corBolinhaAcesa;
			}
			else
			{
				bolinhas[i].color = corBolinhaApagada;
			}
			
		}
	}
}
