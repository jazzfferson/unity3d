using UnityEngine;
using System.Collections;


public class Menu : MonoBehaviour
{
    public AudioSource audio;

    public OratorMessage oratorExit;
	
    public static Menu instance;
	
    [SerializeField]
    private UISprite incognitoLevel;
	
    [SerializeField]	
    private UISprite setaBack;
   
    [SerializeField]
    private UISprite setaNext;
	
    [SerializeField]
    private UILabel numeroFase;
	
    [SerializeField]
    private Transform[] pivos;

    
    private int indexFase = 1;
    bool canPlay = true;
    bool canExit = true;
	
	
    [SerializeField]
    private Transform casasRoot;
	
    [SerializeField]
    private float espacamentoCasa;
	
    private int tamanhoTabuleiro = 5;
	
    [SerializeField]
    private float tamanhoCasa;
	
    [SerializeField]	
    private GameObject casa;
    
    [SerializeField]
    private Fases fases;
	
    GameObject[,] arrayCasas;


    IEnumerator Start()
    {
		
	if(instance==null)
		{
			instance = this;
		}


    Proprietes.MusicFadeIn(audio);

	if(Proprietes.estadoMenu == EstadoMenu.Escolhefase)
		{
			 foreach (Transform pivo in pivos)
           		 Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(-700, 0, 0), false).setEaseType(GoEaseType.CubicInOut).onComplete(Completed => { ButtonSelectLevel.instance.Animacao(); }));
		}
        incognitoLevel.alpha = 0;

        if (Fases.FasesDestravadas > 1)

            indexFase = Fases.FasesDestravadas;
        else
        {
            indexFase = 1;
        }

        numeroFase.text = indexFase.ToString();
        CheckSeta();
        Proprietes.canClick = true;
        MontarCasas();
        MudarTabuleiro();
        yield return new WaitForSeconds(0.2f);
        LevelSelector.instance.Stars(Fases.leveis[indexFase - 1].BestScore);
    }

    void Facebook()
    {
       #if UNITY_ANDROID
        Application.OpenURL("https://www.facebook.com/ShinblackGames");


		/*const string AppId = "645888228767706";
		const string ShareUrl = "http://www.facebook.com/dialog/feed";
		
		const string link = "https://www.facebook.com/ShinblackGames";
		const string pictureLink = "";
		const string name = "Shinblack";
		const string caption = "Somos Foda";
		const string description = "Sem descricao";
		const string redirectUri = "https://www.facebook.com";
		
			Application.OpenURL(ShareUrl +
			                    "?app_id=" + AppId +
			                    "&amp;link=" + WWW.EscapeURL( link )+
			                    "&amp;picture=" + WWW.EscapeURL(pictureLink) +
			                    "&amp;name=" + WWW.EscapeURL(name) +
			                    "&amp;caption=" + WWW.EscapeURL(caption) +
			                    "&amp;description=" + WWW.EscapeURL(description) +
			                    "&amp;redirect_uri=" + WWW.EscapeURL(redirectUri));*/

		#endif

		#if UNITY_WP8
		WP8Statics.FacebookLike();
		#endif
		

    }

    void Like()
    {
		#if UNITY_ANDROID
                 Application.OpenURL("https://play.google.com/store/apps/details?id=com.shinblack.BubbleNoble");
		#endif

		#if UNITY_WP8
		WP8Statics.ReviewMarketPlace();
		#endif
    }

    void NextLevel()
    {



        if (!Proprietes.canClick || !(indexFase < Fases.NumeroDeFases))
            return;

        Instanciador.instancia.PlaySfx(4, 1, 1);
        indexFase++;
        numeroFase.text = indexFase.ToString();
        CheckSeta();



        if (indexFase > Fases.FasesDestravadas)
        {
            canPlay = false;
            TrocarLevelSprite(true);
            ApagarTabuleiro();
            LevelSelector.instance.Stars(0);
        }
        else
        {
            canPlay = true;
            TrocarLevelSprite(false);
            MudarTabuleiro();
            LevelSelector.instance.Stars(Fases.leveis[indexFase - 1].BestScore);
        }
    }

    void PreviousLevel()
    {

        if (!Proprietes.canClick || !(indexFase > 1))
            return;


        Instanciador.instancia.PlaySfx(4, 1, 1);
        indexFase--;
        numeroFase.text = indexFase.ToString();
        CheckSeta();


        if (indexFase <= Fases.FasesDestravadas)
        {
            canPlay = true;
            TrocarLevelSprite(false);
            MudarTabuleiro();
            LevelSelector.instance.Stars(Fases.leveis[indexFase - 1].BestScore);
    
        }
        else
        {
            canPlay = false;
            TrocarLevelSprite(true);
            ApagarTabuleiro();
            LevelSelector.instance.Stars(0);

        }
    }

    void CheckSeta()
    {
        if (indexFase <= 1)
        {
            setaBack.alpha = 0.1f;
        }
        else
        {
            setaBack.alpha = 1f;
        }
        if (indexFase >= Fases.NumeroDeFases)
        {
            setaNext.alpha = 0.1f;
        }
        else
        {
            setaNext.alpha = 1f;
        }
    }

    void PlayJogo()
    {
        if (!canPlay || !Proprietes.canClick)
            return;
        
			
        Instanciador.instancia.PlaySfx(3, 0.4f, 1);
        Fases.faseAtual = indexFase - 1;
        if (WP8Statics.hasInternetConection)
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Advertising", 0.3f);
        else
        {
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Joguinho", 0.3f);
        }

        Proprietes.MusicFadeOut(audio);
    }


    bool btnCanClick = true;
    public void PlaySelectLevel()
    {
        if (!btnCanClick || !Proprietes.canClick)
            return;

        btnCanClick = false;

	Proprietes.estadoMenu = EstadoMenu.Escolhefase;
		
        Instanciador.instancia.PlaySfx(3, 0.4f, 1);
        foreach (Transform pivo in pivos)
            Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(-700, 0, 0), false).setEaseType(GoEaseType.CubicInOut).onComplete(Completed => { ButtonSelectLevel.instance.Animacao(); btnCanClick = true; }));
    }

    public void MenuPrincipal()
    {

        LevelSelector.instance.CancelAnimation();

	    Instanciador.instancia.PlaySfx(3, 0.4f, 1);

        foreach (Transform pivo in pivos)
            Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(0, 0, 0), false).setEaseType(GoEaseType.CubicInOut));

        Proprietes.estadoMenu = EstadoMenu.Inicial;

        
    }
	
    public void TutorialCena()
	{
	
       
	ButtonOptions.instance.Close();
	Instanciador.instancia.PlaySfx(3, 0.4f, 1);
        foreach (Transform pivo in pivos)
           
             Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(700, 0, 0), false).setEaseType(GoEaseType.CubicInOut).onComplete(Completed => { ButtonSelectLevel.instance.Animacao();canPlay = true;}));
		
		Proprietes.estadoMenu = EstadoMenu.Tutorial;
	}
	
    void TrocarLevelSprite(bool Travado)
    {
        LevelSelector.instance.LockedLevel(Travado);
    }

    void Update()
	{
		if(Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
            print("Saves Deletados !!!");
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote))
		{
			if(Proprietes.estadoMenu != EstadoMenu.Inicial)
			{
				Invoke("MenuPrincipal",0.1f);
				Tutorial.instance.Quit();
			}
		
			else 
			{
				if(!canClick)
					return;
				
				canClick = false;
				Invoke("CanClick",1f);
				
				if(!pressedQuit)
				    Pressed();
				else
				{
					
				   No();	
				}
			}
		}
	
	}

    void MontarCasas()
    {
        arrayCasas = new GameObject[tamanhoTabuleiro, tamanhoTabuleiro];

        for (int i = 0; i < tamanhoTabuleiro; i++)
        {
            for (int j = 0; j < tamanhoTabuleiro; j++)
            {
                arrayCasas[i, j] = Instantiate(casa, new Vector3(casasRoot.position.x + (i * espacamentoCasa), casasRoot.position.y, casasRoot.position.z + (j * espacamentoCasa)), Quaternion.identity) as GameObject;
                arrayCasas[i, j].transform.parent = casasRoot;
                arrayCasas[i, j].transform.localRotation = new Quaternion(0, 0, 0, 0);
                arrayCasas[i, j].transform.localScale = new Vector3(tamanhoCasa, tamanhoCasa, tamanhoCasa);

            }
        }

    }

    void MudarTabuleiro()
    {

        if (incognitoLevel.alpha > 0)
        {
            TweenAlpha.Begin(incognitoLevel.gameObject, 0.4f, 0);
        }

        for (int i = 0; i < tamanhoTabuleiro; i++)
            for (int j = 0; j < tamanhoTabuleiro; j++)
            {

                arrayCasas[i, j].GetComponent<CasaMenu>().Liga(false);

                foreach (CasaAtiva ativa in Fases.leveis[indexFase - 1].CasasAtivas)
                {
                    if (i == ativa._i && j == ativa._j)
                    {
                        arrayCasas[i, j].GetComponent<CasaMenu>().Liga(true);
                    }

                }
            }
    }

    void ApagarTabuleiro()
    {
        TweenAlpha.Begin(incognitoLevel.gameObject, 0.4f, 0.6f);
        for (int i = 0; i < tamanhoTabuleiro; i++)
            for (int j = 0; j < tamanhoTabuleiro; j++)
            {
                arrayCasas[i, j].GetComponent<CasaMenu>().Liga(false);
            }
    }
	
	bool pressedQuit = false;

	bool canClick = true;
	
	void Pressed()
	{
		Proprietes.canClick = false;
		pressedQuit = true;
		ButtonOptions.instance.Close();
		Instanciador.instancia.PlaySfx(3,0.4f,1);
		oratorExit.ExibeMessage("");
	}
	
	void No()
	{	
		Proprietes.canClick = true;
		pressedQuit =false;
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
	
	void CanClick()
	{
		canClick = true;
	}
}
