using UnityEngine;
using System.Collections;

public class Obstaculo : MonoBehaviour {

    public float Altura
    {
        get;
        set;
    }
    public Transform cima,baixo;

	void Update () {

        if (!GameFuu.Playing)
            return;
        transform.position += Vector3.left * GameFuu.GameSpeed;

        if (transform.position.x <= GameFuu.PosicaoDestroyObstaculo)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, Altura);
	}

    public void Move(float Y,float duration,float delay)
    {
        if (GameFuu.Playing)
            Go.to(this, duration, new GoTweenConfig().floatProp("Altura",Y, false).setEaseType(GoEaseType.ElasticInOut).setDelay(delay));   
    }
    public void Move(float Y)
    {
        Altura = Y;
    }
    public void Distancia(float distancia)
    {
        cima.transform.position = new Vector3(cima.transform.position.x, distancia);
        baixo.transform.position = new Vector3(cima.transform.position.x,-distancia);
    }

}
