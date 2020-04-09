using UnityEngine;
using System.Collections;

public enum EstadoCamera {LookTabuleiro,FocadoJogador,Padrao};

public class CamJogo : MonoBehaviour {

    public EstadoCamera estadoCamera;
	public Transform targetTabuleiro;
    [HideInInspector]public Transform targetJogador;
    [HideInInspector] Transform targetAtual;

	Vector3 posicaoOriginal;
	float zoomOriginalORT;
	float zoomOriginalPER;
	
	 float distancia;
     float currentRotationAngleY;
     float currentRotationAngleX;
     [HideInInspector]public bool hasTarget;
	
	public float CurrentRotationAngleX
	{
		get{return currentRotationAngleX;}
		set{currentRotationAngleX = value;}
	}
	public float CurrentRotationAngleY
	{
		get{return currentRotationAngleY;}
		set{currentRotationAngleY = value;}
	}
	public float Distancia
	{
		get{return distancia;}
		set{distancia = value;}
	}
	

    // Use this for initialization
	void Start () {
		
        targetAtual = new GameObject().transform;
		posicaoOriginal = gameObject.transform.position;
		zoomOriginalORT = Camera.main.orthographicSize;
        zoomOriginalPER = Camera.main.fieldOfView;

	
	}

	void LateUpdate () {


            UpdateComum();
            TouchCamera();
		
	}
	
	void Update()
	{
		
	}
    public void MudarDeEstado(EstadoCamera estado, Transform target)
    {
        targetJogador = target;

        switch (estado)
        {
            case EstadoCamera.FocadoJogador:

                LookAtCamera(targetAtual, targetJogador, 5, iTween.EaseType.easeOutCubic);
                AnguloCameraX(156,5, iTween.EaseType.easeOutCubic);
                AnguloCameraY(11, 5, iTween.EaseType.easeOutCubic);
                DistanciaTween(10, 4, iTween.EaseType.easeOutCubic);
                estadoCamera = estado;
                break;

            case EstadoCamera.LookTabuleiro:

                LookAtCamera(targetAtual, targetTabuleiro, 2, iTween.EaseType.easeInCubic);
                AnguloCameraX(30, 3, iTween.EaseType.easeInCubic);
                AnguloCameraY(315, 3, iTween.EaseType.easeInCubic);
                DistanciaTween(300, 3, iTween.EaseType.easeInCubic);
                estadoCamera = estado;
                break;

            case EstadoCamera.Padrao:

                LookAtCamera(targetAtual, targetJogador, 4, iTween.EaseType.easeOutCubic);
                AnguloCameraX(134, 3, iTween.EaseType.easeOutCubic);
                AnguloCameraY(90, 3, iTween.EaseType.easeOutCubic);
                DistanciaTween(80, 4, iTween.EaseType.easeOutCubic);
                estadoCamera = estado;
                break;
        }
    }

    public void LookAtCamera(Transform TargetAtual, Transform TargetDestino, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", TargetAtual.position.x, "to", TargetDestino.position.x, "time", Tempo, "onupdate", "LookAtParamCamX", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);

        Hashtable ht2 = iTween.Hash("from", TargetAtual.position.y, "to", TargetDestino.position.y, "time", Tempo, "onupdate", "LookAtParamCamY", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht2);

        Hashtable ht3 = iTween.Hash("from", TargetAtual.position.z, "to", TargetDestino.position.z, "time", Tempo, "onupdate", "LookAtParamCamZ", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht3);
    }

    public void ZoomCamera(float Zoom, float Tempo, iTween.EaseType EaseType)
    {
         Hashtable ht;

        if (!Camera.main.orthographic)
        {
            ht = iTween.Hash("from", Camera.main.fieldOfView, "to", Zoom, "time", Tempo, "onupdate", "CallZoomCamera", "easetype", EaseType);
        }
        else
        {
           ht = iTween.Hash("from", Camera.main.orthographicSize, "to", Zoom, "time", Tempo, "onupdate", "CallZoomCamera", "easetype", EaseType);
        }

        iTween.ValueTo(gameObject, ht);
    }

    public void PosicaoCamera(Transform TargetDestino, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", gameObject.transform.position.x, "to", TargetDestino.position.x, "time", Tempo, "onupdate", "PosicaoCameraX", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);

        Hashtable ht2 = iTween.Hash("from", gameObject.transform.position.y, "to", TargetDestino.position.y, "time", Tempo, "onupdate", "PosicaoCameraY", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht2);

        Hashtable ht3 = iTween.Hash("from", gameObject.transform.position.z, "to", TargetDestino.position.z, "time", Tempo, "onupdate", "PosicaoCameraZ", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht3);
    }

    public void AnguloCameraX(float angulo, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", currentRotationAngleX, "to", angulo, "time", Tempo, "onupdate", "AnguloCameraXCall", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);
    }

    public void AnguloCameraY(float angulo, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", currentRotationAngleY, "to", angulo, "time", Tempo, "onupdate", "AnguloCameraYCall", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);
    }

    void DistanciaTween(float distan, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", Distancia, "to", distan, "time", Tempo, "onupdate", "DistanciaCall", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);
    }

    #region CallBacks Do Reset da Camera

     void AnguloCameraYCall(float newValue)
    {
        currentRotationAngleY = newValue;
    }

     void AnguloCameraXCall(float newValue)
    {
       
        currentRotationAngleX = newValue; 
    }

     void DistanciaCall(float valor)
     {
         Distancia = valor;
     }

	 void updateFromValue3(float newValue)
    {
        Camera.main.orthographicSize = newValue; 
    }

	 void updateFromValue4(float newValue)
    {
        Camera.main.fieldOfView = newValue;
    }

    #endregion

    #region  CallBacks Dos ParametrosCamera

    void LookAtParamCamX(float valor)
    {
        targetAtual.position = new Vector3(valor, targetAtual.position.y, targetAtual.position.z);
    }
    void LookAtParamCamY(float valor)
    {
        targetAtual.position = new Vector3(targetAtual.position.x,valor, targetAtual.position.z);
    }
    void LookAtParamCamZ(float valor)
    {
        targetAtual.position = new Vector3(targetAtual.position.x, targetAtual.position.y,valor);
    }

    void CallZoomCamera(float valor)
    {
        if (!Camera.main.orthographic)
        {
            Camera.main.fieldOfView = valor;
        }
        else
        {
            Camera.main.orthographicSize = valor;
        }
    }
    #endregion

    #region CallBacks Do PosicaoCamera

    void PosicaoCameraX(float valor)
    {
        gameObject.transform.position = new Vector3(valor, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    void PosicaoCameraY(float valor)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, valor, gameObject.transform.position.z);
    }
    void PosicaoCameraZ(float valor)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, valor);
    }

    #endregion

    #region Update

    void UpdateComum()
    {
		
		
            var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);
           

                this.gameObject.transform.position = targetAtual.position;
                this.gameObject.transform.position -= currentRotation * Vector3.forward * Distancia;
                this.gameObject.transform.LookAt(targetAtual);


    }

    #endregion
	
	#region Controles
	void Cima()
	{
		
		 Go.to(this,0.3f,
			new GoTweenConfig().floatProp("CurrentRotationAngleX",
			currentRotationAngleX+10,false).setEaseType(GoEaseType.CubicInOut));
		
			
	}
	void Baixo()
	{
		
		Go.to(this,0.3f,
			new GoTweenConfig().floatProp("CurrentRotationAngleX",
			CurrentRotationAngleX-10,false).setEaseType(GoEaseType.CubicInOut));
	}
	void Esquerda()
	{
		
		 Go.to(this,0.3f,
			new GoTweenConfig().floatProp("CurrentRotationAngleY",
			CurrentRotationAngleY+10,false).setEaseType(GoEaseType.CubicInOut));
	}
	void Direita()
	{
		
		Go.to(this,0.3f,
			new GoTweenConfig().floatProp("CurrentRotationAngleY",
			CurrentRotationAngleY-10,false).setEaseType(GoEaseType.CubicInOut));
	}
	void ZoomIn()
	{
		
		Go.to(this,0.3f,
			new GoTweenConfig().floatProp("Distancia",
			Distancia-10,false).setEaseType(GoEaseType.CubicInOut));
	}
	void ZoomOut()
	{
		
		 Go.to(this,0.3f,
			new GoTweenConfig().floatProp("Distancia",
			Distancia+10,false).setEaseType(GoEaseType.CubicInOut));
	}
	
    void TouchCamera()
    {
		
		/*
        if(Input.GetKey(KeyCode.UpArrow))
          {
              currentRotationAngleX+= 50 *Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.DownArrow))
          {
              currentRotationAngleX -= 50 * Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.RightArrow))
          {
              currentRotationAngleY -= 50 * Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.LeftArrow))
          {
              currentRotationAngleY += 50 * Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.Alpha1))
          {

              distancia += 20 * Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.Alpha2))
          {
              distancia -= 20 * Time.deltaTime;
          }
      */
    }
	#endregion

}