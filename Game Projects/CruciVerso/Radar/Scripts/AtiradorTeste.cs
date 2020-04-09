using UnityEngine;
using System.Collections;

public class AtiradorTeste : MonoBehaviour
{

    public Light luzTiro;
    public GameObject tiro;
    GameObject instancia;
    float timer;
    public float intervaloDisparo;
    public float precisao;
    public float velocidadeMedia;
    public float tempoVida;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.05f)
        {
            luzTiro.intensity = 2.2f;

            if (timer <= 0)
            {

                luzTiro.intensity = 0f;
                instancia = (GameObject)Instantiate(tiro, gameObject.transform.position, Quaternion.identity);
                instancia.transform.parent = gameObject.transform;
                instancia.GetComponent<Tiro>().Init(precisao, velocidadeMedia, tempoVida);
                timer = Random.Range(intervaloDisparo / 50, intervaloDisparo / 30);

            }

        }

    }
}
