using UnityEngine;
using System.Collections;

public class Alien_Grande : MonoBehaviour
{

    #region Atributos

    public GameObject personagem;
    float sensibilidadeX = 350;
    float sensibilidadeY = 450;
    float velocidadeMaxima = 3f;
    float offsetY = 0.4f;
    float minimoParaMover = 0.9f;

    #endregion


    void Start()
    {

        personagem.animation.Play("Default Take");
    }


    void Update()
    {
		
        //MovimentacaoTeclado();
        MovimentacaoAcelerometro();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ColisorDano")
        {
            GameData.numVidas--;
            Pontuacao.instance.AtualizarPontuacao();
        }

    }


    void MovimentacaoTeclado()
    {
        Vector3 inputDirecao = new Vector3();
        inputDirecao.x = Input.GetAxis("Horizontal");
        inputDirecao.y = Input.GetAxis("Vertical");

        //this.transform.Translate(posAcelerom.x * Time.deltaTime * sensibilidade, 0, posAcelerom.y * Time.deltaTime * sensibilidade);
        //gameObject.GetComponentInChildren<CharacterController>().Move(new Vector3(posAcelerom.x * sensibilidadeX, 0, (posAcelerom.y + offsetY) * sensibilidadeY) * Time.deltaTime);
        gameObject.GetComponentInChildren<CharacterController>().Move(new Vector3(inputDirecao.x * 100, 0, inputDirecao.y * 100) * Time.deltaTime);
        personagem.gameObject.transform.LookAt(personagem.gameObject.transform.position + gameObject.GetComponentInChildren<CharacterController>().velocity.normalized);
        float speed = Mathf.Abs(gameObject.GetComponentInChildren<CharacterController>().velocity.magnitude / 30);
        speed = Mathf.Clamp(speed, 0, velocidadeMaxima);
        personagem.animation["Default Take"].speed = speed;
    }


    void MovimentacaoAcelerometro()
    {
        //capturar o valor do acelerometro
        var posAcelerom = Acelerometro.Valor;

        if (posAcelerom.magnitude > minimoParaMover)
        {        
            //this.transform.Translate(posAcelerom.x * Time.deltaTime * sensibilidade, 0, posAcelerom.y * Time.deltaTime * sensibilidade);
            gameObject.GetComponentInChildren<CharacterController>().Move(new Vector3(posAcelerom.x * sensibilidadeX, 0, (posAcelerom.y + offsetY) * sensibilidadeY) * Time.deltaTime);
            personagem.gameObject.transform.LookAt(personagem.gameObject.transform.position + gameObject.GetComponentInChildren<CharacterController>().velocity.normalized);
            float speed = Mathf.Abs(gameObject.GetComponentInChildren<CharacterController>().velocity.magnitude / 30);
            speed = Mathf.Clamp(speed, 0, velocidadeMaxima);
            personagem.animation["Default Take"].speed = speed;
        }
        else
        {
            personagem.animation["Default Take"].speed = 0;
        }
    }

}
