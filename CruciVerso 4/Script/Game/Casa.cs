using UnityEngine;
using System.Collections;
public enum EstadoCasa {CasaColor,AndarSelecionado,CasaSelecionada,CasaDisponivel,Nave1,Nave2,Nenhum}; 



public class Casa : MonoBehaviour {
	
	#region Variaveis da cor da casa e suas transições
	public Color AndarSelecionado;
	public Color Selecionada;
	public Color Disponivel;
	public Color Nave1;
	public Color Nave2;
	public float AlphaNenhum;
	public float TempoTrasicaoNenhum;
	public iTween.EaseType TipoTransicaoNenhum;
	#endregion
	
	[HideInInspector]public Vector3 ID;

    [HideInInspector] public bool interacaoDisponivel = false;
    [HideInInspector] public bool disponivelParaMover = false;
    [HideInInspector] public bool andarAtual;
    [HideInInspector] public bool interagivel=true;
    [HideInInspector] public GameObject jogadorAtual = null;
	
	public EstadoCasa estadoCasa = EstadoCasa.Nenhum;
    [HideInInspector] public Color casaColor;
    Color corOriginal;
	
	void Start () {

        corOriginal = gameObject.GetComponent<UISprite>().color;
        casaColor = corOriginal;

        foreach (UIButtonMessage script in gameObject.GetComponents<UIButtonMessage>())
        {
            script.target = GameObject.Find("Main"); 
        }

        
	}

    public void Cor(float red, float green, float blue,float alpha)
	{
		
		
		gameObject.GetComponent<UISprite>().color = new Color(red,green,blue,alpha);
        casaColor = gameObject.GetComponent<UISprite>().color;
	}
	
	public void Alpha(float alpha,float tempoTransicao,iTween.EaseType tipo)
	{

		
        casaColor = gameObject.GetComponent<UISprite>().color;
		gameObject.GetComponent<UISprite>().color = new Color(casaColor.r,casaColor.g,casaColor.b,alpha);
		
		/*Hashtable ht = iTween.Hash("from", casaColor.a, "to", alpha, "time", tempoTransicao, "onupdate", "updateFromValue", "easetype",tipo);
		iTween.ValueTo(gameObject,ht);*/
	}

	public void VisibilidadeCasa(bool visibilidade)
	{
		if(visibilidade)
		{
       		 gameObject.GetComponent<UISprite>().alpha = 1;
		}
		else
		{
			gameObject.GetComponent<UISprite>().alpha = 0;
		}
	}
	
	public void AndarAtual(bool isActual)
	{
		if(isActual)
		{
			TrocarEstadoCasa(EstadoCasa.AndarSelecionado);
			GetComponent<UISprite>().enabled=true;
		}
		else
		{
			TrocarEstadoCasa(EstadoCasa.Nenhum);
			GetComponent<UISprite>().enabled=false;
			
		}
			andarAtual = isActual;
	}

    public void TrocarEstadoCasa(EstadoCasa estado)
	{
        estadoCasa = estado;

            switch (estado)
            {
                case EstadoCasa.CasaColor:

                    if (andarAtual)
                    {
                        Cor(corOriginal.r, corOriginal.g, corOriginal.b, casaColor.a);
                    }
                    else
                        Cor(corOriginal.r, corOriginal.g, corOriginal.b, AlphaNenhum);

                    break;

                case EstadoCasa.AndarSelecionado:

                    Alpha(AndarSelecionado.a, TempoTrasicaoNenhum, TipoTransicaoNenhum);

                    break;

                case EstadoCasa.CasaSelecionada:

                    Cor(Selecionada.r, Selecionada.g, Selecionada.b, Selecionada.a);

                    break;

                case EstadoCasa.CasaDisponivel:

                    Cor(Disponivel.r, Disponivel.g, Disponivel.b, AndarSelecionado.a);
                  
                    break;

                case EstadoCasa.Nenhum:

                    Alpha(AlphaNenhum, TempoTrasicaoNenhum, TipoTransicaoNenhum);

                    break;

                case EstadoCasa.Nave1:

                    Cor(Nave1.r, Nave1.g, Nave1.b, Nave1.a);

                    break;

                case EstadoCasa.Nave2:

                    Cor(Nave2.r, Nave2.g, Nave2.b, Nave2.a);

                    break;
        }
	}
	
	void updateFromValue(float valor)
	{
	
		gameObject.GetComponent<UISprite>().alpha = valor;

       if (gameObject.GetComponent<UISprite>().alpha <= 0)
        {
            gameObject.GetComponent<UISprite>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<UISprite>().enabled = true;
        }
	}
	
}
