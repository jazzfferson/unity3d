using UnityEngine;
using System.Collections;

public class Lancador : MonoBehaviour {
	
	
	#region Variaveis Publicas
	public GameObject Bola;
    public GameObject Seta;
	public Vector3 PosicaoLancamento;
	//public Vector3 posicaoMaximaLancador;
	public float rotacaoMaximaLancador;
    public GameObject cam;
	
	#endregion
	
	#region Variaveis Privadas

	Vector3 lookAt;
	bool lancado=true;
    float quantidadeRotacao;
	[HideInInspector]public GameObject BolaRef;
	#endregion
	// Use this for initialization
	void Start () {
				
           gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            lookAt = gameObject.transform.position + new Vector3(0, 0, 20);
            quantidadeRotacao = 0;
            Seta.transform.position = gameObject.transform.position;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
			
			
           
			cam.GetComponent<Camera>().GetComponent<CameraJazz>().target = BolaRef.transform;
            cam.GetComponent<Camera>().GetComponent<CameraJazz>().ativa = false;
            cam.GetComponent<Camera>().GetComponent<CameraJazz>().ResetarCamera();
            iTween.MoveAdd(BolaRef.gameObject, iTween.Hash("name", "tweenBola", "x", -PosicaoLancamento.x*2, "y", 0, "z", 0, "time", 2, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
            lancado = false;
		
	}
	void Awake()
	{
		CriarBola();   
	}

	void LateUpdate()
	{		
		if(!lancado)
		{
			/*//Limita a posicao do Lancador eixo X
			Bola.gameObject.transform.position = new Vector3(Mathf.Clamp(Bola.gameObject.transform.position.x,-posicaoMaximaLancador.x,posicaoMaximaLancador.x),
			Bola.gameObject.transform.position.y,
			Bola.gameObject.transform.position.z);
		
			//Limita a posicao do Lancador eixo Y
			Bola.gameObject.transform.position = new Vector3(Bola.gameObject.transform.position.x,
			Mathf.Clamp(Bola.gameObject.transform.position.y,-posicaoMaximaLancador.y,posicaoMaximaLancador.y),
			Bola.gameObject.transform.position.z);
		
			//Limita a posicao do Lancador eixo z
			Bola.gameObject.transform.position = new Vector3(Bola.gameObject.transform.position.x,
			Bola.gameObject.transform.position.y,
			Mathf.Clamp(Bola.gameObject.transform.position.z,-posicaoMaximaLancador.z,posicaoMaximaLancador.z));*/
		}
						
	}
	void Update()
	{
        ChecagemEstado();
		
	}
	
	void ChecagemEstado()
	{
        if (!lancado)
        {
            Seta.transform.position = BolaRef.transform.position;
        }
	}
	public void Rotaciona(Vector2 quantidade)
	{
		lookAt+=new Vector3(quantidade.x/10,0,0);
		lookAt.x=Mathf.Clamp(lookAt.x,-rotacaoMaximaLancador,rotacaoMaximaLancador);
		//lookAt.y=Mathf.Clamp(lookAt.y,-rotacaoMaximaLancador,rotacaoMaximaLancador);
        quantidadeRotacao = lookAt.x;
		gameObject.transform.LookAt(lookAt);
	}
	public void Move(Vector3 quantidade)
	{
		if(!lancado)
		Bola.gameObject.transform.Translate(quantidade*Time.deltaTime,Space.World);
	}
	public void Lancar(float intensidade)
	{
		if(!lancado)
		{
            cam.GetComponent<Camera>().GetComponent<CameraJazz>().ativa = true;
			lancado=true;
			iTween.Stop(BolaRef);
			gameObject.GetComponentInChildren<Renderer>().enabled=false;		
			BolaRef.GetComponent<Rigidbody>().isKinematic=false;
            BolaRef.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, intensidade), ForceMode.Impulse);
            BolaRef.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -quantidadeRotacao*100), ForceMode.Impulse);
         
		}
	}
    public void ResetarBolaCamera()
    {

        if (lancado)
        {
			if(BolaRef!=null)
			iTween.Stop(BolaRef);
			
			CriarBola();
			
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            lookAt = gameObject.transform.position + new Vector3(0, 0, 20);
            quantidadeRotacao = 0;
            Seta.transform.position = gameObject.transform.position;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
			
			
           
			cam.GetComponent<Camera>().GetComponent<CameraJazz>().target = BolaRef.transform;
            cam.GetComponent<Camera>().GetComponent<CameraJazz>().ativa = false;
            cam.GetComponent<Camera>().GetComponent<CameraJazz>().ResetarCamera();
            iTween.MoveAdd(BolaRef.gameObject, iTween.Hash("name", "tweenBola", "x", -PosicaoLancamento.x*2, "y", 0, "z", 0, "time", 2, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
            lancado = false;
        }

    }
	void CriarBola()
	{
		    BolaRef = (GameObject)Instantiate(Bola);
			BolaRef.GetComponent<Rigidbody>().isKinematic = true;
            BolaRef.transform.position = gameObject.transform.position + PosicaoLancamento;
            BolaRef.transform.rotation = new Quaternion(0, 0, 0, 0);
	}

}
