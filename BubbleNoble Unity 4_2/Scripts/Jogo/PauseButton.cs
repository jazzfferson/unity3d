using UnityEngine;
using System.Collections;
using System;

public class PauseButton : MonoBehaviour {
	
	public static PauseButton instance;
	public UISprite iconPlay;
	public UISprite iconPause;
	public UIPanel infoPausePanel;
	public UILabel infoPauseLabel;
	public UIPanel[] painelBotoes;
	public Transform[] botoesTela2;
	public UISprite fadePretoCanto;
	public UISprite fadeTelaToda;
	bool paused = false;
	bool canClick = true;
	
	void Start () {
		
		if(instance==null)
		{
			instance = this;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote) && (Proprietes.estadoJogo!= EstadoJogo.WinScreen))
		Pause();

	}
	
	void Pause()
	{
		if(!canClick || Proprietes.estadoJogo == EstadoJogo.WinScreen)
		    return;
		
		canClick = false;

		infoPauseLabel.text = Fases.leveis[Fases.faseAtual].jogadasMinimas.ToString();

		if(Fases.faseAtual+2>Fases.FasesDestravadas)
		{
			ButtonNext.instance.Enable(false);
		}
		else
		{
			ButtonNext.instance.Enable(true);
		}
		
		
		float alphaBGPause;
		float alphaBgTotal;
		float alphaBotaoPlay;
		float alphaBotaoPause;
		float alphaPanelBotoes;
		float scaleBotoes;
		
		if(!paused)
		{

			WP8Statics.EnableAd1();
			// Evita que o faca jogada nas casas enquanto esta pausado.
			Proprietes.canClick = false;
			buttonIndex = true;
			paused = true;
			Proprietes.estadoJogo = EstadoJogo.Pausado;
			
			StartCoroutine(rotinaClick(1));
			
			 alphaBGPause = 0.6f;
			 alphaBgTotal= 0.9f;
			 alphaBotaoPlay = 1f;
			 alphaBotaoPause = 0f;
			 alphaPanelBotoes = 1;
			 scaleBotoes = 1;
			
			for(int i =0 ; i < botoesTela2.Length;i++)
			{
				Go.to(painelBotoes[i],Proprietes.menuButtonTimeScale/2, new GoTweenConfig().floatProp("alpha",alphaPanelBotoes).setEaseType(GoEaseType.CubicInOut).setDelay(i*Proprietes.menuButtonTimeInterval));
				Go.to(botoesTela2[i].transform,Proprietes.menuButtonTimeScale,new GoTweenConfig().scale(scaleBotoes,false).setEaseType(GoEaseType.ElasticOut).setDelay(i*Proprietes.menuButtonTimeInterval).onStart(start=>OnStartTween()));
			}


		}
		else
		{

			WP8Statics.DisableAd1();
			
			StartCoroutine(rotinaClick(0.6f));
			Proprietes.canClick = true;
			
			paused = false;
			Proprietes.estadoJogo = EstadoJogo.Jogando;
			
			 alphaBGPause = 0;
			 alphaBgTotal= 0f;
			 alphaBotaoPlay = 0f;
			 alphaBotaoPause = 1f;
			 alphaPanelBotoes = 0;
			 scaleBotoes = 0;
			
			Instanciador.instancia.PlaySfx(3,0.8f,2f);
			
			for(int i =0 ; i < botoesTela2.Length;i++)
			{
				Go.to(painelBotoes[i],Proprietes.menuButtonTimeScale/2, new GoTweenConfig().floatProp("alpha",alphaPanelBotoes).setEaseType(GoEaseType.QuartOut));
				Go.to(botoesTela2[i].transform,Proprietes.menuButtonTimeScale,new GoTweenConfig().scale(scaleBotoes,false).setEaseType(GoEaseType.QuartOut));
			}
		}
		
		// Alpha dos backgrounds
		TweenAlpha.Begin(fadePretoCanto.gameObject,0.2f,alphaBGPause);
		TweenAlpha.Begin(fadeTelaToda.gameObject,0.5f,alphaBgTotal);
		// Alpha do icone do botao
		
		TweenAlpha.Begin(iconPlay.gameObject,0.2f,alphaBotaoPlay);
		TweenAlpha.Begin(iconPause.gameObject,0.2f,alphaBotaoPause);

		//Alpha da informacao de jogadas
		float delay;
		if(alphaBotaoPlay==0)
			delay = 0;
		else
			delay = 0.3f;
		
		TweenAlpha.Begin(infoPausePanel.gameObject,0.2f,alphaBotaoPlay).delay= delay;
		
		
	}
	
    IEnumerator rotinaClick(float time)
	{
		yield return new WaitForSeconds(time);
		canClick = true;
	}
	bool buttonIndex;

	void OnStartTween()
	{
		if(buttonIndex && Fases.faseAtual+2>Fases.FasesDestravadas)
		{

		}
		else
		{
			Instanciador.instancia.PlaySfx(3,0.8f,1.7f);
		}
		buttonIndex = false;

	}

}
