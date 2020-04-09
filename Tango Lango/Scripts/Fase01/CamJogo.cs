using UnityEngine;
using System.Collections;

public enum EstadoCamera {Modo1,Modo2,Modo3};

public class CamJogo : MonoBehaviour {

    public EstadoCamera estadoCamera;
	Transform targetJogo;
    [HideInInspector]public Transform targetJogador;
    [HideInInspector] Transform targetAtual;

	Vector3 posicaoOriginal;
	float zoomOriginalORT;
	float zoomOriginalPER;
	
	 float distancia;
     float currentRotationAngleY;
     float currentRotationAngleX;

     [HideInInspector]public bool hasTarget = false;
	
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
        targetAtual.position = Vector3.zero;

      
		posicaoOriginal = gameObject.transform.position;
		zoomOriginalORT = Camera.main.orthographicSize;
        zoomOriginalPER = Camera.main.fieldOfView;

      

        

	
	}
   
    #region Tudo
    // Update is called once per frame
	void Update () {

        print(currentRotationAngleX);

            UpdateComum();
		
	}
    public void MudarDeEstado(EstadoCamera estado, Transform target)
    {
        targetJogador = target;

        switch (estado)
        {

            case EstadoCamera.Modo1:

               // LookAtCamera(targetAtual, target, 2, iTween.EaseType.easeInCubic);
              //  AnguloCameraX(90, 3, iTween.EaseType.easeInCubic);
               // AnguloCameraY(0, 3, iTween.EaseType.easeInCubic);


               
               


                estadoCamera = estado;
                break;

            case EstadoCamera.Modo2:

                LookAtCamera(targetAtual, target, 2, iTween.EaseType.easeInCubic);
                AnguloCameraX(16, 3, iTween.EaseType.easeInCubic);
                AnguloCameraY(0, 3, iTween.EaseType.easeInCubic);
                Go.to(this.transform, 3, new GoTweenConfig().position(new Vector3(0, 40, 0.4f), false));


                estadoCamera = estado;
                break;

         

            case EstadoCamera.Modo3:

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

        if (!Camera.main.isOrthoGraphic)
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
       // currentRotationAngleY = newValue;
    }

     void AnguloCameraXCall(float newValue)
    {
       
        //currentRotationAngleX = newValue; 
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
        if (!Camera.main.isOrthoGraphic)
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

         this.transform.rotation = new Quaternion(currentRotationAngleX, currentRotationAngleY,0,0);
    }

    #endregion
    #endregion


}