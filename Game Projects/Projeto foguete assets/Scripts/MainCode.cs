using UnityEngine;
using System.Collections;
public enum EstadoJogo{Pausado,Jogo,Resultado};
public class MainCode : MonoBehaviour
{
	public static MainCode instance;
	public EstadoJogo estadoJogo;
	public bool canPause;

    #region Variaveis
	[SerializeField]
    private PauseGame pauseGame;
	[SerializeField]
    private ResultsPanel resultsPanel;
	[SerializeField]
    private GameObject explosion;
	[SerializeField]
    private Foguete foguete;
	[SerializeField]
    private PathViewer pathViewerFoguete;
	[SerializeField]
    private CameraOffset camera;
	[SerializeField]
    private Transform planetaLogico;
	[SerializeField]
    private Transform planetaGrafico;
	[SerializeField]
    private UIPanel uiMovablePanel;
	[SerializeField]
    private UIPanel uiStaticPanel;
	[SerializeField]
    private UIPanel fxStaticPanel;

    private Quaternion rotacaoDesejada;
    private GameObject landTarget;
    private bool landed;
    private bool canLand;
	private bool morreu;

    private float forca;
    private float tempoPropulsao;
    private Vector3 posicaoFoguete;
    private bool hasfinishedAnimation = false;
    private bool executeAnimation = true;
    private float distaciaChecarLand;
    private Vector3 posDesejada;
    private GoTween tween;
	private GoTween tweenField;
	
	private GameObject cenario;
	private FieldForce fieldForceAtual;
	

    #endregion
	
	[SerializeField]
	private UISlider forceSlider, timeSlider,angleSlider;

	#region Propriedades
	
	public float uiMovablePanelAlpha
	{
		get;
		set;
	}
	public float uiStaticPanelAlpha
	{
		get;
		set;
	}
	public float fxStaticPanelAlpha
	{
		get;
		set;
	}

	
	#endregion
	
	void Start () {
		
		canPause = false;
		
		if(instance==null)
			instance=this;
		estadoJogo = EstadoJogo.Jogo;
		
		if(InfoFases.FaseJogo==null)
		{
			InfoFases.Initialize();
			InfoFases.Load();
		}
		

        #region Eventos
        foguete.OnMorreu += HandleFogueteOnMorreu;
        foguete.OnLancado += HandleFogueteOnLancado;
        foguete.OnColisorCenario += HandleFogueteOnColisorCenario;
        foguete.OnGenericCollision += HandleFogueteOnGenericCollision;

        resultsPanel.OnClose += resultsPanel_OnClose;
        resultsPanel.OnNextLevel += resultsPanel_OnNextLevel;
        resultsPanel.OnRestart += resultsPanel_OnRestart;

        pauseGame.OnRestart += new PauseGame.PauseGameEventHandler(pauseGame_OnRestart);
        #endregion
		
		
		cenario = CriarCenario((InfoFases.FaseAtual+1).ToString());
		
		ReInitializeLevel();
		
		

	}

    #region Eventos
	
	void pauseGame_OnRestart()
    {
        ReInitializeLevel();
    }
    void resultsPanel_OnRestart()
    {
        ReInitializeLevel();
        resultsPanel.HideResults();
    }
    void resultsPanel_OnNextLevel()
    {
		InfoFases.FaseAtual+=1;
        Application.LoadLevel("Jogo");
    }
    void resultsPanel_OnClose()
    {
        Application.LoadLevel("Menu");
    }

	void HandleFogueteOnMorreu(GameObject sender)
	{
		
		
		 if (!landed)
			{
				
				foguete.rigidbody.isKinematic = true;
				foguete.mesh.renderer.enabled = false;
				Instantiate(explosion,foguete.transform.position,Quaternion.identity);
                resultsPanel.ShowResults(ResetDistanciaPercorrida(), ResetTimer(), true);
				morreu = true;
				canLand = false;
			}
		
		
	}
	
	void HandleFogueteOnLancado(GameObject sender)
	{
		 pathViewerFoguete.enabled = true;
	}
	
	void HandleFogueteOnColisorCenario(GameObject sender)
	{
		 foguete.rigidbody.drag = 0.1f;
		 canLand = true;	
	}
	
	void HandleFogueteOnGenericCollision(GameObject sender)
	{
	
        if (sender.name == landTarget.name && canLand)
        {
			
				tween.pause();

                resultsPanel.ShowResults(ResetDistanciaPercorrida(), ResetTimer(), false);
                InfoFases.FasesHabilitadas = Mathf.Clamp(InfoFases.FaseAtual + 2, InfoFases.FasesHabilitadas, 6);
			
				if(sender.name!="Terra")
                foguete.transform.parent = sender.transform;
				else
				foguete.transform.parent =	planetaGrafico.transform;
			
				InfoFases.Save();
            
        }	
		 
	}
	
	void OnSliderChange(float valor)
	{
        forca = valor/50f;
	}
	
	void OnSliderChange2(float valor)
	{
		tempoPropulsao = valor * 5;
	}	
	
	
    #endregion
	
	#region Metodos
	void Update () {

		
        Timer();
        DistanciaPercorrida();

		LandedAnimation();
		
		uiStaticPanel.alpha = uiStaticPanelAlpha;
        uiMovablePanel.alpha = uiMovablePanelAlpha;
		
	}
	
	void Lauch()
	{
        StartTimer();
		foguete.Lauch(forca,tempoPropulsao);
		Go.to(this,0.5f,new GoTweenConfig().floatProp("uiStaticPanelAlpha",0,false));
        Go.to(this, 1f, new GoTweenConfig().floatProp("uiMovablePanelAlpha", 0, false));

        camera.follow = true;
	}
	
	void ReInitializeLevel()
	{

		ResetSliders();
		morreu = false;
        camera.enabled = true;
		
		if(tween!=null)
		tween.destroy();
		
		if(tweenField!=null)
		tweenField.destroy();
		
		Go.to(this,0.8f,new GoTweenConfig().floatProp("uiMovablePanelAlpha",1,false));
		
        ResetTimer();
        camera.enabled = true;
        camera.follow = false;
		executeAnimation = true;
		canLand = false;
        landed = false;
		foguete.isAnimating = false;
		
		planetaLogico.transform.rotation = Quaternion.identity;
		foguete.Reset(planetaLogico);
        pathViewerFoguete.Reset();
		pathViewerFoguete.enabled = false;
		Go.to(this,0.2f,new GoTweenConfig().floatProp("uiStaticPanelAlpha",1,false).setEaseType(GoEaseType.CubicInOut));
		
		float timeScale = 0.6f;
		GetComponent<FieldForce>().CreateField(landTarget.transform, 8,1,8,5,0.4f,3,0.1f * timeScale,0.8f* timeScale,2,2* timeScale,0,360,GoEaseType.CubicInOut);
	}
	
	void LandedAnimation()
	{	

		if(!canLand)
			return;
		
        float distance = Vector3.Distance(foguete.transform.position,landTarget.transform.position);

        if (distance < distaciaChecarLand)
		{
            if (foguete.rigidbody.velocity.magnitude > 0.3 && executeAnimation)
                foguete.rigidbody.drag += Time.deltaTime * foguete.rigidbody.velocity.magnitude * 30;
            else
            {
                if (executeAnimation)
                {
                    
                    foguete.isAnimating = true;
                    foguete.rigidbody.isKinematic = true;
                    rotacaoDesejada = Quaternion.LookRotation(foguete.transform.position - landTarget.transform.position);
                    Go.to(foguete.transform, 3, new GoTweenConfig().rotation(rotacaoDesejada, false).setEaseType(GoEaseType.CubicInOut).onComplete(completou => FinishedFirstAnimation()));
                    executeAnimation = false;
					
                }

            }
		}

	}
	
	void FinishedFirstAnimation()
	{
		
		if(!morreu)
		{
			landed = true;
        	camera.follow = false;
       	 	camera.enabled = false;
        	Vector3 posicaoCamera = new Vector3(landTarget.transform.position.x,landTarget.transform.position.y,camera.transform.position.z);
        	Go.to(camera.transform, 1f, new GoTweenConfig().position(posicaoCamera, false));
			pathViewerFoguete.enabled = false;
			posDesejada = foguete.transform.position - foguete.transform.forward * 60;
			tween = Go.to(foguete.transform,4,new GoTweenConfig().position(posDesejada,false).setEaseType(GoEaseType.CubicIn));
		}
	}
	
	GameObject CriarCenario(string LevelName)
	{

		
		GameObject instancia;
		//Carregando o cenario, instanciando-o e fazendo-o filho do transform.
		instancia = (GameObject)Instantiate(Resources.Load("Props Fase"+LevelName, typeof(GameObject)));
		instancia.transform.parent = transform;
		instancia.transform.localPosition = Vector3.zero;
		//Procurando o gameobject onde a nave deve pousar e calculando a distancia baseado no tamanho do objeto
        landTarget = GameObject.Find(InfoFases.FaseJogo[InfoFases.FaseAtual].landTargetName);	
        distaciaChecarLand = landTarget.transform.localScale.x;
		
		return instancia;
	}
	
    #region Timer Partida
    float totalTime;
    bool timer;

    void Timer()
    {
        if (timer)
        {
            totalTime += Time.deltaTime;
        }
    }
    float ResetTimer()
    {
        float result = totalTime;
        totalTime = 0;
        return result;
    }
    void StartTimer()
    {
        timer = true;
    }

    #endregion

    #region DistanciaPercorrida

    float distanciaPercorrida;

    void DistanciaPercorrida()
    {
        distanciaPercorrida += foguete.rigidbody.velocity.magnitude * Time.deltaTime* 100000;
    }
    float ResetDistanciaPercorrida()
    {
        float result = distanciaPercorrida;
        distanciaPercorrida = 0;
        return result;
    }
    



    #endregion

   #endregion	
	
	
	
	
	
	void ResetSliders()
	{
		Go.to (forceSlider,0.2f,new GoTweenConfig().floatProp("value",0,false).setDelay(0.5f).setEaseType(GoEaseType.CubicOut));
		Go.to (timeSlider,0.2f,new GoTweenConfig().floatProp("value",0,false).setDelay(0.5f).setEaseType(GoEaseType.CubicOut));
		Go.to (angleSlider,0.2f,new GoTweenConfig().floatProp("value",0.5f,false).setDelay(0.5f).setEaseType(GoEaseType.CubicOut));
	}

}
