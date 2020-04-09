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
	public float distancia;
    public float currentRotationAngleY;
    public float currentRotationAngleX;


    #region TouchCamera


    #endregion

    // Use this for initialization
	void Start () {

        targetAtual = new GameObject().transform;
		
		posicaoOriginal = gameObject.transform.position;
		zoomOriginalORT = Camera.mainCamera.orthographicSize;
		zoomOriginalPER = Camera.mainCamera.fieldOfView;

	
	}
	
	// Update is called once per frame
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
                Distancia(10, 4, iTween.EaseType.easeOutCubic);
                estadoCamera = estado;
                break;

            case EstadoCamera.LookTabuleiro:

                LookAtCamera(targetAtual, targetTabuleiro, 2, iTween.EaseType.easeInCubic);
                AnguloCameraX(30, 3, iTween.EaseType.easeInCubic);
                AnguloCameraY(315, 3, iTween.EaseType.easeInCubic);
                Distancia(300, 3, iTween.EaseType.easeInCubic);
                estadoCamera = estado;
                break;

            case EstadoCamera.Padrao:

                LookAtCamera(targetAtual, targetJogador, 4, iTween.EaseType.easeOutCubic);
                AnguloCameraX(134, 3, iTween.EaseType.easeOutCubic);
                AnguloCameraY(90, 3, iTween.EaseType.easeOutCubic);
                Distancia(80, 4, iTween.EaseType.easeOutCubic);
                estadoCamera = estado;
                break;
        }
    }

    void LookAtCamera(Transform TargetAtual, Transform TargetDestino, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", TargetAtual.position.x, "to", TargetDestino.position.x, "time", Tempo, "onupdate", "LookAtParamCamX", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);

        Hashtable ht2 = iTween.Hash("from", TargetAtual.position.y, "to", TargetDestino.position.y, "time", Tempo, "onupdate", "LookAtParamCamY", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht2);

        Hashtable ht3 = iTween.Hash("from", TargetAtual.position.z, "to", TargetDestino.position.z, "time", Tempo, "onupdate", "LookAtParamCamZ", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht3);
    }

    void ZoomCamera(float Zoom,float Tempo, iTween.EaseType EaseType)
    {
         Hashtable ht;

        if (!Camera.mainCamera.isOrthoGraphic)
        {
            ht = iTween.Hash("from", Camera.mainCamera.fieldOfView, "to", Zoom, "time", Tempo, "onupdate", "CallZoomCamera", "easetype", EaseType);
        }
        else
        {
           ht = iTween.Hash("from", Camera.mainCamera.orthographicSize, "to", Zoom, "time", Tempo, "onupdate", "CallZoomCamera", "easetype", EaseType);
        }

        iTween.ValueTo(gameObject, ht);
    }

    void PosicaoCamera(Transform TargetDestino, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", gameObject.transform.position.x, "to", TargetDestino.position.x, "time", Tempo, "onupdate", "PosicaoCameraX", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);

        Hashtable ht2 = iTween.Hash("from", gameObject.transform.position.y, "to", TargetDestino.position.y, "time", Tempo, "onupdate", "PosicaoCameraY", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht2);

        Hashtable ht3 = iTween.Hash("from", gameObject.transform.position.z, "to", TargetDestino.position.z, "time", Tempo, "onupdate", "PosicaoCameraZ", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht3);
    }

    void AnguloCameraX(float angulo, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", currentRotationAngleX, "to", angulo, "time", Tempo, "onupdate", "AnguloCameraXCall", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);
    }

    void AnguloCameraY(float angulo, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", currentRotationAngleY, "to", angulo, "time", Tempo, "onupdate", "AnguloCameraYCall", "easetype", EaseType);
        iTween.ValueTo(gameObject, ht);
    }

    void Distancia(float distan, float Tempo, iTween.EaseType EaseType)
    {
        Hashtable ht = iTween.Hash("from", distancia, "to", distan, "time", Tempo, "onupdate", "DistanciaCall", "easetype", EaseType);
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
         distancia = valor;
     }

	 void updateFromValue3(float newValue)
    {
        Camera.mainCamera.orthographicSize = newValue; 
    }
	 void updateFromValue4(float newValue)
    {
        Camera.mainCamera.fieldOfView = newValue;
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
        if (!Camera.mainCamera.isOrthoGraphic)
        {
            Camera.mainCamera.fieldOfView = valor;
        }
        else
        {
            Camera.mainCamera.orthographicSize = valor;
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

    #region ComunCamera

    void UpdateComum()
    {
        var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);
        transform.position = targetAtual.position;
        transform.position -= currentRotation * Vector3.forward * distancia;
        transform.LookAt(targetAtual);
    }

    #endregion

    void TouchCamera()
    {
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
          else if(Input.GetKey(KeyCode.F1))
          {

              distancia += 20 * Time.deltaTime;
          }
          else if(Input.GetKey(KeyCode.F2))
          {
              distancia -= 20 * Time.deltaTime;
          }
      
    }

}