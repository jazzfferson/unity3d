using UnityEngine;
using System.Collections;

public class Ufo : MonoBehaviour
{

    #region Atributos

    int tirosMetralhadora;
    float cargaLaser;
    bool metralhadora;
    public GameObject laser;
    private GameObject[] PosicoesPossiveis;
    public GameObject jogador;
    public ParticleSystem particulas;
    public LineRenderer raio;
    Transform posicaoRaio;
    public bool KillerAutomatico;
    public GameObject colisorRaio;
    public GameObject ControleAudio;

    #endregion


    void Start()
    {

        particulas.enableEmission = false;

        PosicoesPossiveis = GameObject.FindGameObjectsWithTag("UfoPosition");

        //StartCoroutine(TrocaPosicao(5f));
        StartCoroutine(AnimacaoInicial());

        //começa com a metralahdora
        tirosMetralhadora = 50;
        cargaLaser = 50;
        metralhadora = true;

        //inicializar o target do raio
        posicaoRaio = new GameObject().transform;
        KillerAutomatico = false;
    }


    // Update is called once per frame
    void Update()
    {

        CalculoRaio();
        raio.SetPosition(0, transform.position);
        raio.SetPosition(1, posicaoRaio.transform.position);
        particulas.gameObject.transform.position = posicaoRaio.transform.position;
        colisorRaio.transform.position = posicaoRaio.transform.position;

        if (KillerAutomatico)
        {
            //checagens da metralhadora
            if (metralhadora && tirosMetralhadora == 50)
            {
                StartCoroutine(AtirarRotina()); //se der problemas, checar se o 20 é o total
			laser.SetActive(true);
                raio.enabled = false;
                particulas.enableEmission = false;
                colisorRaio.SetActive(false);
            }
            else if (metralhadora && tirosMetralhadora == 0)
            {
                //se os tiros da metralhadora acabaram, enche a carga do laser
                cargaLaser = 50;
                metralhadora = false;
                StopCoroutine("AtirarRotina");
            }
            else if (!metralhadora && cargaLaser == 50)
            {
                StartCoroutine(AtivarLaser());
                particulas.enableEmission = true;
                colisorRaio.SetActive(true);
                raio.enabled = true;
                laser.SetActive(false);
                //audio raio laser
                ControleAudio.GetComponent<SoundController>().PlaySfx(1, 1);
                
            }
            else if (!metralhadora && cargaLaser <= 0)
            {
                //trocar para a matralhadora
                metralhadora = true;
                tirosMetralhadora = 50;

                StopCoroutine("AtivarLaser");

            }
        }
    }

    IEnumerator AnimacaoInicial()
    {
        yield return new WaitForSeconds(0);
        // a pessoa que eu quero mecher, o tempo do deslocamento, iniciar parametros. setEaseType = deixar suave 
        //Go.to(this.transform,6f, new GoTweenConfig().shake(new Vector3(4f, 2f, 4f), GoShakeType.Eulers, 3));
        //Go.to(this.transform, 4f, new GoTweenConfig().position(new Vector3(0, 9, 0), false).setEaseType(GoEaseType.CubicInOut));
        Go.to(this.transform, 2f, new GoTweenConfig().position(PosicoesPossiveis[Random.Range(0, PosicoesPossiveis.GetLength(0))].transform.position, false).setEaseType(GoEaseType.CubicInOut).setDelay(0));
        StartCoroutine(TrocaPosicao(3f));
    }

    IEnumerator TrocaPosicao(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        KillerAutomatico = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Go.to(this.transform, 1.5f, new GoTweenConfig().position(PosicoesPossiveis[Random.Range(0, PosicoesPossiveis.GetLength(0))].transform.position, false).setEaseType(GoEaseType.ExpoInOut));
        StartCoroutine(TrocaPosicao(0));
    }



    IEnumerator AtivarLaser()
    {
        posicaoRaio.position = new Vector3(transform.position.x, 0, transform.position.z);
        Go.to(posicaoRaio, 0.3f, new GoTweenConfig().position(jogador.transform.position, false));
        cargaLaser -= 10;
        yield return new WaitForSeconds(2);
        if (cargaLaser > 0)
        {
            StartCoroutine(AtivarLaser());

        }
    }

    void Atirar()
    {
        GameObject laserAtual = (GameObject)Instantiate(laser, this.transform.position, Quaternion.identity);
        laserAtual.transform.LookAt(jogador.transform);
        laserAtual.GetComponent<Laser>().velocidade = 250f;
        tirosMetralhadora--;
                    //audio raio laser
			SoundController.instance.PlaySfx(3, 1);
    }

    IEnumerator AtirarRotina()
    {

        Atirar();
        yield return new WaitForSeconds(0.3f);
        //se aidna tem tiros
        if (tirosMetralhadora > 0)
        {
            StartCoroutine(AtirarRotina());
        }


    }

    void CalculoRaio()
    {
        Ray cursorRay = new Ray(this.transform.position, this.transform.TransformDirection(posicaoRaio.position));

        RaycastHit hit;

        if (Physics.Raycast(cursorRay, out hit))
        {
            if (hit.collider.name == "ColisorPersonagem")
            {

                print("AcertouPersonagem");
            }
        }
    }


}
