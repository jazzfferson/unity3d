using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

        public UIPanel painelPivo;
	public UIPanel painelLabels;
	public UIPanel[] painelBotoes;
	public UISprite[] stars;
	public Transform[] botoesTela;
	public Transform[] botoesTela2;
	
	public UILabel primaryLabel;
	public UILabel secondaryLabel;
	public UISprite fade;
	
	public UISprite orator;
	public ParticleSystem[] particulasTiro;
	public ParticleSystem particulasCaindo;
	public GameObject congrates;
	public Transform congratesPivo;
	public GoEaseType easeMessageScale;

	bool allStars;

	public void Play(bool newLevelUnlocked)
	{

		indexParticulas = 0;

		StartCoroutine(StartAnimation(newLevelUnlocked));

		if(Fases.faseAtual<50)
		{
			if(Fases.faseAtual+2>Fases.FasesDestravadas)
			{
				ButtonNext.instance.Enable(false);
			}
			else
			{
				ButtonNext.instance.Enable(true);
			}
		}
		else
		{
			ButtonNext.instance.Enable(false);
		}
	}
	IEnumerator StartAnimation(bool newLevelUnlocked)
	{
		
	     #region Animacao dos botoes da tela
		for(int i =0 ; i < botoesTela.Length;i++)
		{
			Go.to(botoesTela[i].transform,0.2f,new GoTweenConfig().setEaseType( GoEaseType.QuadIn ).position( new Vector3( 1.5f, 0, 0 ),true ).setDelay(i * 0.2f)); 
		}
		
		primaryLabel.text = randPhrase();
		#endregion
		
	     #region Animacao do Orator  
	     TweenAlpha.Begin(orator.gameObject,0.2f,1f);
	     Go.to(orator.transform,0.5f,new GoTweenConfig().scale(0.8f,false).setEaseType(GoEaseType.ElasticOut).setDelay(0.2f));
	     #endregion
		
	     #region Animacao do Pivo
	     Go.to(painelPivo,0.2f,new GoTweenConfig().floatProp("alpha",1,false).setDelay(0.4f));
	     Go.to(congratesPivo,1.2f,new GoTweenConfig().scale(1f,false).setEaseType(easeMessageScale).setDelay(0.4f));
		#endregion
		
	     yield return new WaitForSeconds(0.8f);
		
	     TweenAlpha.Begin(primaryLabel.gameObject,0.5f,1);
	     Go.to(painelLabels,0.2f,new GoTweenConfig().floatProp("alpha",1,false));
		
		
	    //Exibe advertising
	    WP8Statics.EnableAd1();
		
	     yield return new WaitForSeconds(0.2f);
		
		StarsEarned(Fases.leveis[Fases.faseAtual].Score);
		
	      #region Animacao fade da tela
 	     TweenAlpha.Begin(fade.gameObject,0.5f,0.7f);

	
	
	      #endregion
		
	     yield return new WaitForSeconds(1f);



	     #region Animacao de qual mensagem exibir

	    if(newLevelUnlocked)
	     {
		if(Fases.faseAtual==50)
			{
				secondaryLabel.text = "NEW LEVELS\nCOMING SOON";
			}
			else
		secondaryLabel.text = "NEW LEVEL\nUNLOCKED";
	     }
	      else
		{
			secondaryLabel.text = "GOOD GAME!";
		}
		
		
		TweenAlpha.Begin(secondaryLabel.gameObject,0.5f,1).delay = 0.4f;
	     #endregion
		
	     for(int i =0 ; i < botoesTela2.Length;i++)
		{
			Go.to(painelBotoes[i],Proprietes.menuButtonTimeScale/2, new GoTweenConfig().floatProp("alpha",1).setEaseType(GoEaseType.CubicInOut).setDelay(i*Proprietes.menuButtonTimeInterval));
			Go.to(botoesTela2[i].transform,Proprietes.menuButtonTimeScale,new GoTweenConfig().scale(1,false).setEaseType(GoEaseType.ElasticOut).setDelay(i*Proprietes.menuButtonTimeInterval).onStart(start=>{Instanciador.instancia.PlaySfx(3,0.8f,1.7f);}));
		}

	}	
	string randPhrase()
	{
		int rand = Random.Range(0,3);
		
		if(Fases.leveis[Fases.faseAtual].Score == 3)
		{
		
			if(rand == 1)
			{
				return "YOU ARE THE MAN!";
			}
			else if(rand == 2)
			{
				return "LIKE A BOSS!";
			}
			else
			{
				return "EXCELLENT!";
			}
		}
		
		else if(Fases.leveis[Fases.faseAtual].Score == 2)
		{
		
			if(rand == 1)
			{
				return "VERY GOOD!";
			}
			else if(rand == 2)
			{
				return "GREAT!";
			}
			else
			{
				return "AWESOME!";
			}
		}
		
		else if(Fases.leveis[Fases.faseAtual].Score == 1)
		{
		
			if(rand == 1)
			{
				return "GOOD!";
			}
			else if(rand == 2)
			{
				return "NICE!";
			}
			else
			{
				return "NOT BAD!";
			}
		}
		else
		{
			if(rand == 1)
			{
				return "NOT TOO BAD!";
			}
			else if(rand == 2)
			{
				return "PASSABLE!";
			}
			else
			{
				return "TOLERABLE!";
			}
		}
	}
	void StarsEarned(int quant)
	{

		if(quant==3)
		{
			allStars = true;
			particulasCaindo.Play();
		}

		quant = Mathf.Clamp(quant,0,3);
		float time = 0.6f;
		float delay = 0.3f;
		GoEaseType ease = GoEaseType.ElasticOut;

		
		
		for(int i = 0 ; i < quant; i++)
		{
			Go.to(stars[i].transform,time,new GoTweenConfig().scale(0.4937938f,false).setDelay(i*delay).setEaseType(ease).onStart(Started=>PlayParticles()));
			Go.to(stars[i],time/3,new GoTweenConfig().colorProp("color",Color.white).setDelay(i*delay).setEaseType(ease));

			Instanciador.instancia.PlaySfx(6,0.4f,(i+1) * 0.8f,i*delay);
		}
	}	
	void Menu()
	{
		Proprietes.estadoMenu = EstadoMenu.Escolhefase;
		WP8Statics.DisableAd1();
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu",0.8f);
	}
	
	bool retryPressed = false;
	
	void Retry()
	{
		if(retryPressed)
			return;
		retryPressed = true;
		 WP8Statics.DisableAd1();
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		LoadScreenManager.instance.LoadScreenWithFadeInOut("Joguinho",0.8f);
	}
	
	bool nextPressed = false;
	
	void Next()
	{
		if(nextPressed)
			return;
		
		nextPressed = true;
		 WP8Statics.DisableAd1();
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		Fases.faseAtual = Fases.faseAtual+1;
		LoadScreenManager.instance.LoadScreenWithFadeInOut("Joguinho",0.8f);
	}
	
	void Update()
	{
		if(Proprietes.estadoJogo == EstadoJogo.WinScreen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote)))
		{
			Menu();
		}
	}

	int indexParticulas;

	void PlayParticles()
	{
		if(!allStars)
			return;
		particulasTiro[indexParticulas].Play();
		Instanciador.instancia.PlaySfx(7,0.5f,1f);
		indexParticulas++;

	}
		
}
