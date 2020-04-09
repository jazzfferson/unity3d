using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

    public GameObject[] astesArray;
    public string tagTarget;
    List<GameObject> targetsArray;

    public GameObject TargetVisual;
    public float velocidadeAstes;
    public int distanciaRadar;

    int asteAtual = 0;
    Ray ray;
    RaycastHit hit;
    Vector3 direcao;

    Color corAste;

    [HideInInspector]public GameObject TargetEncontrado;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
	void Start () {
        targetsArray = new List<GameObject>();
        corAste = astesArray[0].GetComponent<UISprite>().color;
        InitRadar();    
	}

	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Controle();
        }

        if (Input.GetMouseButtonDown(1))
        {

            InitRadar();
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -10 * Time.deltaTime, 0);
        }
        else if (Input.GetKey("right"))
        {
            transform.Rotate(0, 10 * Time.deltaTime, 0);
        }
        CalculoAlphaTargets();    
	}

    void InitRadar()
    {
        targetsArray.Clear();
        foreach (GameObject possivelTagert in GameObject.FindGameObjectsWithTag(tagTarget))
        {
            if (Vector3.Distance(possivelTagert.transform.position, gameObject.transform.position) < distanciaRadar)
            {
                targetsArray.Add(possivelTagert);
            }
        }

        asteAtual = 0;
        TargetVisual.SetActive(false);
        TargetVisual.GetComponent<TweenAlpha>().Reset();

        for(int i = 0 ; i < astesArray.Length; i++)
        {
            astesArray[i].GetComponent<TweenAlpha>().Reset();
            astesArray[1].GetComponent<TweenTransform>().Reset();
            astesArray[1].GetComponent<TweenTransform>().enabled = false;
            astesArray[i].GetComponent<TweenTransform>().duration = velocidadeAstes;
            astesArray[i].GetComponent<UISprite>().color = corAste;
            astesArray[i].GetComponent<TweenAlpha>().enabled = false;
            
        }

        astesArray[0].GetComponent<TweenTransform>().enabled = true;

        foreach (GameObject targ in targetsArray)
        {
            targ.GetComponent<Target>().TargetMode(true);
            targ.GetComponent<Target>().TweenAlpha(0.1f, 0);
           
        }

    }

    void Controle()
    {
        

        if (asteAtual == 0)
        {
            astesArray[asteAtual].GetComponent<TweenTransform>().enabled = false;
            
            asteAtual++;

            astesArray[asteAtual].GetComponent<TweenTransform>().enabled = true;
        }

        else if (asteAtual == 1)
        {
            astesArray[asteAtual].GetComponent<TweenTransform>().enabled = false;
            astesArray[0].GetComponent<TweenAlpha>().enabled = true;
            astesArray[1].GetComponent<TweenAlpha>().enabled = true;

            TargetVisual.transform.position = new Vector3(astesArray[0].transform.position.x, astesArray[1].transform.position.y, TargetVisual.transform.position.z);
            direcao = TargetVisual.transform.position - transform.position;
            direcao.Normalize();

            if (Physics.Raycast(transform.position, direcao, out hit, distanciaRadar))
            {
                
             
                TargetEncontrado = hit.collider.gameObject;


                hit.collider.gameObject.GetComponent<Target>().Color(new Color(1, 0, 0),false);
                
                
                TargetVisual.SetActive(true);
                TargetVisual.GetComponent<TweenAlpha>().to = 0.1f;
                TargetVisual.GetComponent<TweenAlpha>().from = 1;
                TargetVisual.GetComponent<UISprite>().color = new Color(1, 0, 0, 1);
                TargetVisual.GetComponent<TweenAlpha>().enabled = true;
                TargetVisual.GetComponent<TweenAlpha>().style = UITweener.Style.PingPong;
                TargetVisual.GetComponent<TweenAlpha>().duration = 0.2f;
            }

            else
            {
             
                TargetVisual.SetActive(true);
                TargetVisual.GetComponent<TweenAlpha>().to = 0;
                TargetVisual.GetComponent<TweenAlpha>().from = 1;
                TargetVisual.GetComponent<UISprite>().color = new Color(0,1,0,1);
                TargetVisual.GetComponent<TweenAlpha>().enabled = true;
                TargetVisual.GetComponent<TweenAlpha>().style = UITweener.Style.Once;
                TargetVisual.GetComponent<TweenAlpha>().duration = 2f;
            }

        
        }

    }

    void CalculoAlphaTargets()
    {
        
        Vector3 posAste = astesArray[asteAtual].transform.position;
        Vector2 AstePosTela = WorldToScreen(posAste);
        

        foreach (GameObject targ in targetsArray)
        {
            Vector3 targMin = targ.collider.bounds.min;
            Vector3 targMax = targ.collider.bounds.max;

            Vector2 targtMinTela = WorldToScreen(targMin);
            Vector2 targtMaxTela = WorldToScreen(targMax);


            if (AstePosTela.x > targtMinTela.x && AstePosTela.x < targtMaxTela.x && asteAtual == 0 &&  targ.GetComponent<Target>().tweenRodando==false)
            {
                targ.GetComponent<Target>().TweenAlpha(0.2f,2,1);
                
            }
            else if (AstePosTela.y > targtMinTela.y && AstePosTela.y < targtMaxTela.y && asteAtual == 1 && targ.GetComponent<Target>().tweenRodando == false)
            {
                targ.GetComponent<Target>().TweenAlpha(0.2f, 2, 1);
                
            }
            else
            {
               
            }
            
          
        }
    }

    Vector2 WorldToScreen(Vector3 posicaoMundo)
    {
        return camera.WorldToScreenPoint(posicaoMundo);
    }
}
