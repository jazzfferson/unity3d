using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    [SerializeField]
    private UISprite fadeSprite;

    [SerializeField]
    private Rigidbody fogueteRigidBody;

    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private float timeTransition;

    private Vector3 originalPosition;
    TweenPosition actualTween;


    private Vector3 velocity;
    private Vector3 angularVelocity;
    private float drag;

	void Start () {
	
        originalPosition = transform.localPosition;
        actualTween = GetComponent<TweenPosition>();
	}
	
	
	void Update () {


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote))
        {
            Pause();
        }
       
	}

    void Pause()
    {
		if(MainCode.instance.estadoJogo == EstadoJogo.Jogo)
		{
			
			MainCode.instance.estadoJogo = EstadoJogo.Pausado;
		
       	 	TweenAlpha.Begin(fadeSprite.gameObject, 0.4f, 0.8f);

        	velocity = fogueteRigidBody.velocity;
        	angularVelocity = fogueteRigidBody.angularVelocity;
        	drag = fogueteRigidBody.drag;
        	fogueteRigidBody.isKinematic = true;

        	actualTween.from = transform.localPosition;
        	actualTween.to = position;
        	actualTween.duration = timeTransition;

        	actualTween.enabled = true;
        	actualTween.Reset();
        	actualTween.Play();
        	Time.timeScale = 0;
   
        	actualTween.callWhenFinished = "";
		}

    }
	
    void ReturnGame()
    {
		MainCode.instance.estadoJogo =EstadoJogo.Jogo;
		
        TweenAlpha.Begin(fadeSprite.gameObject, 0.4f, 0);

        actualTween.from = transform.localPosition;
        actualTween.to = originalPosition;
        actualTween.duration = timeTransition;

        actualTween.enabled = true;
        actualTween.Reset();
        actualTween.Play();

        actualTween.callWhenFinished = "TerminouTween";
        actualTween.eventReceiver = gameObject;
        
    }
	
    void TerminouTween()
    {
		if(fogueteRigidBody.gameObject.GetComponent<Foguete>().haslanched)
		{
        	fogueteRigidBody.isKinematic = false;
        	fogueteRigidBody.velocity = velocity;
        	fogueteRigidBody.angularVelocity = angularVelocity;
        	fogueteRigidBody.drag = drag;
		}
        

        Time.timeScale = 1;
    }

    void Restart()
    {
		MainCode.instance.estadoJogo =EstadoJogo.Jogo;
		
        TerminouTween();
        TweenAlpha.Begin(fadeSprite.gameObject, 0.4f, 0);
        transform.localPosition = originalPosition;

        if (OnRestart != null)
        {
            OnRestart();
        }
    }

    void Menu()
    {
		MainCode.instance.estadoJogo =EstadoJogo.Jogo;
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }

    public delegate void PauseGameEventHandler();
    public event PauseGameEventHandler OnRestart;
    public event PauseGameEventHandler OnQuit;
}
