using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	// Use this for initialization
    float vel;
    float tvid;
	void Start () {

       
        
	
	}

    public void Init(float precisao, float velocidade,float tempoVida)
    {
        transform.Rotate(Random.Range(0, 0), Random.Range(-precisao, precisao), Random.Range(-precisao, precisao));
        vel = velocidade;
        tvid = tempoVida;
        GetComponent<TweenAlpha>().duration = tempoVida;
    }
	// Update is called once pert frame
	void Update () {

        
        
        if (tvid <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.left * Time.deltaTime * vel);

        tvid -= Time.deltaTime;
	}
}
