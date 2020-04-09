using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienPequeno : MonoBehaviour
{

    #region Atributos

    public GameObject particulas;
    public GameObject decal;
    public delegate void EventoAlien(AlienPequeno alienScript);
    public event EventoAlien OnEnterBuraco;
    public event EventoAlien OnMorreu;
    public GameObject sombra;
    public List<GameObject> aranhasColididasComigo;
    public float ID;
	private iTweenEvent tweenCamera;
	

    #endregion

    float count;

    void Start()
    {
		tweenCamera = Camera.main.GetComponent<iTweenEvent>();
        animation["Walking"].speed = 1.8f;
        animation.Play("Walking");
        aranhasColididasComigo = new List<GameObject>(0);
        ID = Time.timeSinceLevelLoad;
    }
    

    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "Entrada")
        {
			
			tweenCamera.Play();
			
			LightFlicker.instance.Flick();
            //tirando a aranha da spinline


            //desativar o BoxCollider
            this.gameObject.collider.enabled = false;

            //posicionando a sombra
            Go.to(sombra.gameObject.renderer, 0.2f, new GoTweenConfig().materialColor(new Color(1, 1, 1, 0)));
			
			
            //setando a proxima animação
			
            animation.Stop();
			GetComponent<AndarRandom>().DestroySelf(1f);
			GetComponent<AndarRandom>().enabled = false;
			transform.LookAt(Vector3.zero);
			animation["Jumping"].speed = 1.8f;
            animation.CrossFade("Jumping");


            Fase01.quantidadeAranhasOnda--;

           GameData.numVidas--;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Lango(Clone)")
        {
            GetComponent<AndarRandom>().TweenAlien.play();
            GetComponent<AndarRandom>().TweenTarget.play();
            animation["Walking"].speed = 1.8f;
            RemoverEstaAranhaDaLista(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {

      

    }
        


    public void Acertou()
    {
        //checagem para garantir que o jogador nao toque na tela, fique com o dedo parado e mate toda a fila q vem atras
        // if (Input.touches[0].position != Game1.posicaoUltimoToque)
        // {
        if (OnMorreu != null) OnMorreu(this);
        GetComponent<AndarRandom>().DestroySelf(0);
        Instantiate(decal, new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), Quaternion.identity);
        GameObject particula = (GameObject)Instantiate(particulas, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(particula, 0.5f);

        SoundController.instance.PlaySfx(Random.Range(0, 2), 1);

        Fase01.quantidadeAranhasOnda--;
        Fase01.numAranhasMortas++;

        Pontuacao.instance.AtualizarPontuacao();

        //            Game1.posicaoUltimoToque = Input.touches[0].position;
        // }
    }


    bool JaChequeiColisaoComEstaAranha(GameObject aranha)
    {
        for (int i = 0; i < aranhasColididasComigo.Count; i++)
        {
            if (aranhasColididasComigo[i] == aranha)
                return true;
        }

        return false;
    }


    void RemoverEstaAranhaDaLista(GameObject aranha)
    {
        for (int i = 0; i < aranhasColididasComigo.Count; i++)
        {
            if (aranhasColididasComigo[i] == aranha)
            {
                aranhasColididasComigo.RemoveAt(i);
                return;
            }
        }
    }


}
