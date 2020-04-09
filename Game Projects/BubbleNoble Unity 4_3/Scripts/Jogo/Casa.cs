using UnityEngine;
using System.Collections;

public class Casa : MonoBehaviour {

	public Casa casaSuperior,casaInferior,casaEsquerda,CasaDireita;
	bool ativa;
    public float transparenciaOff;
    public float speedChange;
    public UISprite CasaGrafica;
    public GoEaseType ease;
    static bool start;
	
	void Start()
	{
       	  CasaGrafica.alpha = transparenciaOff;
	  start = true;
	}
	public void Ativa()
	{
		if(ativa)
		{
			
			ativa = false;
            Transparencia(transparenciaOff);
			Proprietes.casasAtivas--;
		}
		else
		{
			
			ativa = true;
            		Transparencia(1);
			Proprietes.casasAtivas++;
		}	
	}
	public void Click()
	{
        if (!Proprietes.canClick || !(Proprietes.estadoJogo==EstadoJogo.Jogando))
            return;

        if (Clickado!= null)
        {
            Clickado();
        }
		
		
		Ativa();
		AtivarCasas();
                Proprietes.canClick = false;
		Proprietes.jogadasEfetuadas++;
			
	}
	public void AtivarCasas()
	{
		if(casaSuperior!=null)
		{
			casaSuperior.Ativa();
		}
		if(casaInferior!=null)
		{
			casaInferior.Ativa();
		}
		if(casaEsquerda!=null)
		{
			casaEsquerda.Ativa();
		}
		if(CasaDireita!=null)
		{
			CasaDireita.Ativa();
		}
		
	}
        void Transparencia(float alpha)
    {
        TweenAlpha.Begin(CasaGrafica.gameObject, speedChange * 0.8f, alpha);
        Go.to(CasaGrafica.transform, speedChange, new GoTweenConfig().scale(alpha, false).setEaseType(ease).onComplete(Complete => Completed()));
    }
        private void Completed()
    {
               Proprietes.canClick = true;
		
		if(Proprietes.casasAtivas<=0 && start)
		{
			start = false;
			 Proprietes.canClick = false;
			if (Win!= null)
        		{
           	 		Win();
       			}
		}
    }

    public delegate void DelegateCasa();
    public event DelegateCasa Clickado;
    public event DelegateCasa Win;
	

}
