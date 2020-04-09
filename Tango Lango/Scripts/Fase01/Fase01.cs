using UnityEngine;
using System.Collections;

public class Fase01 : MonoBehaviour
{

    public static Fase01 instance;

    #region  Atributos

    public int nivel;
    public static int quantidadeAranhasOnda;
    public static Vector2 posicaoUltimoToque;
    //public GameObject[] marcadoresPosicaoInstanciar;
    public static Vector3[] posicoesInstanciar;
    //public static int faseAtual;
    public static int numAranhasMortas;

    //setar tilling
    float alturInicialCamera;
    public GameObject chao;
    public GameObject ColisorMeio;
    public UILabel TextoTimer;

    #endregion
	
	
	bool next = true;

    void Start()
    {


       // DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }


        quantidadeAranhasOnda = 0;
        GameData.numVidas = 3;
        GameData.faseAtual = 1;
        GameData.tempoFicouVivo = 0;
        numAranhasMortas = 0;
        nivel = 1;
    }


    void Update()
    {
        #region BACK do celular

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu", 2f);
			//Application.LoadLevel("Menu");
        }

        #endregion

        //tempo que ficou vivo
        GameData.tempoFicouVivo += Time.deltaTime;

        //imprimir no Painel
        TextoTimer.text = string.Format("{0:0.0}", GameData.tempoFicouVivo) + " sec";
        
        //checar Gerar uma nova onda
        if (!GetComponent<Gerador>().onda/* && quantidadeAranhasOnda <= 1*/)
        {            
            //gerar onda
            GetComponent<Gerador>().InicarOnda(nivel);
            nivel++;
        }

        //se morreu
        if (GameData.numVidas <= 0 && next)
        {
            ProximaFase();
			next = false;
        }
    }
    

    float ConverterAranhasMatadasParaVida()
    {
        return numAranhasMortas;
    }


    public void ProximaFase()
    {
        //converter as aranhas mortas para vida e salvar no GameData
        GameData.numVidas = ConverterAranhasMatadasParaVida();
		//Application.LoadLevel("Animacao_Transicao_Fase");
        LoadScreenManager.instance.LoadScreenWithFadeInOut("Animacao_Transicao_Fase", 0.001f);
    }

}
