using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AndarRandom : MonoBehaviour
{

    #region Atributos

    GoSpline path;

    public bool animacao;
    Vector3[] arrayPositions;
    Transform target;
    float tempo;
    public GameObject[] marcadoresPosicao;
    
    public GoTween TweenTarget;
    public GoTween TweenAlien;

    #endregion


    void Start()
    {
        animacao = true;
    }


    void Update()
    {
        transform.LookAt(target);
    }


    public void CriarCaminho()
    {
        //tempo = Random.Range(1f * velocidade, 1.5f * velocidade);

        //array de 5 elementos
        float rand = Random.Range(28,32);
		float randVel = Random.Range(0f,2f);

        arrayPositions = new Vector3[5];

        //EU TAMBÉM TENTEI FAZER PARA GERAR A PARTIR DE UM ARRAY DE POSIÇÕES MAS O MELHOR RESULTADO QUE CHEGUEI FOI COM ESSE RANDOM
        Vector3[] arrayTeste = { new Vector3(rand * RandomSinal.Sortear(), 0, rand * RandomSinal.Sortear()), new Vector3(0, 0, 0), new Vector3(rand * RandomSinal.Sortear(), 0, rand * RandomSinal.Sortear()), new Vector3(rand * RandomSinal.Sortear(), 0, rand * RandomSinal.Sortear()) };

        target = new GameObject().transform;

        path = new GoSpline(arrayTeste);

        path.closePath();

        TweenAlien = Go.to(this.transform, 7 - randVel , new GoTweenConfig().positionPath(path, false, GoLookAtType.None).setDelay(0.01f));
        TweenTarget = Go.to(target, 7 - randVel, new GoTweenConfig().positionPath(path, false, GoLookAtType.None));



    }

    
    public void DestroySelf(float time)
    {
		
	    Go.removeTween(TweenTarget);
		Go.removeTween(TweenAlien);
		
        Destroy(target.gameObject, time);
        Destroy(gameObject, time);
    }


    void OnDrawGizmos()
    {
        path.drawGizmos(10);
    }


}



