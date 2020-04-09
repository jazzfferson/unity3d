using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum EstadoJogada {EscolhendoCasa,EscolhendoAcao,Jogando};

public enum EstadoJogo{OutGame,FocoJogador,Jogo};
	
public class Main : MonoBehaviour {

EstadoJogada estadoJogada = EstadoJogada.EscolhendoCasa;
EstadoJogo estadoJogo = EstadoJogo.Jogo;

	
public GameObject NaveAlemao;
public GameObject NaveFrances;
public GameObject Tabuleiro;

public GameObject CameraJogo;
public GameObject CameraRadar;

public GameObject TargetCamera;
	
public GameObject[] Efeitos;
	
private int jogadorAtual = 0;
private int jogadorPosterior = 1;
private int andarAtual = 1;
	
private GameObject[] arrayNaves;
private GameObject[] arrayCamera;
	
int quantCasas = 1;


	// Use this for initialization
	void Start () {
	
	
	CriarNaves();
   
	CriarEventosCasa();

	TargetCameraCalculo();	
	
	CriarCamera();


    //Tabuleiro.GetComponent<Tabuleiro>().TransparenciaCasas(NaveAlemao.transform.position,800,0);

    StartCoroutine(ROTINATESTE());

	}


    #region Parametros do inicio do Jogo padrão

    void ParametrosIniciais()
    {

        arrayCamera[0].GetComponent<CamJogo>().MudarDeEstado(EstadoCamera.Padrao, arrayNaves[jogadorAtual].transform);
        VisibilidadeTabuleiro(true);
        VisibilidadeAndar(1);
        MarcarCasasJogadores();
        Efeitos[0].SetActive(false);
    }

    #endregion

    #region InterfaceJogo

    void TabuleiroLook(GameObject Botao)
    {
        andarAtual++;

        if (andarAtual > 2)
        {
            andarAtual = 0;
        }

        VisibilidadeAndar(andarAtual);

    }

    void Mover(GameObject Botao)
    {
        MarcarCasasDisponiveisMover(1);
        VisibilidadeAndar(andarAtual);
    }

    void Acao(GameObject Botao)
    {
        if (estadoJogo == EstadoJogo.Jogo)
        {
            Estadojogo(EstadoJogo.FocoJogador);
        }
        else if (estadoJogo == EstadoJogo.FocoJogador)
        {
            Estadojogo(EstadoJogo.Jogo);
        }
    }

    void Atacar(GameObject Botao)
    {
        CameraJogo.SetActive(false);
        CameraRadar.SetActive(true);
        CameraRadar.transform.position = arrayNaves[jogadorAtual].transform.position;
        arrayNaves[jogadorAtual].GetComponent<Renderer>().gameObject.SetActive(false);
        VisibilidadeTabuleiro(false);
    }

    #endregion

    #region Courotines

    //Transicao da camera inicial do jogo para a camera que foca o jogador

    IEnumerator ROTINATESTE()
    {
        yield return new WaitForSeconds(0.2f);
        ParametrosIniciais();
    }


    IEnumerator Rotina1(float timeGeral)
    {
       
        yield return new WaitForSeconds(timeGeral);
      //  arrayCamera[0].GetComponent<Crease>().enabled = false;
       // arrayCamera[0].GetComponent<AntialiasingAsPostEffect>().enabled = false;
      //  arrayCamera[0].GetComponent<ColorCorrectionCurves>().enabled = false;



        VisibilidadeTabuleiro(true);
        VisibilidadeAndar(1);
        MarcarCasasJogadores();
       // Efeitos[0].SetActive(false);

    }

    IEnumerator Rotina2(float timeGeral)
    {

        yield return new WaitForSeconds(timeGeral);

        VisibilidadeTabuleiro(false);
       // Efeitos[0].SetActive(true);


       // arrayCamera[0].GetComponent<Crease>().enabled = true;
        //arrayCamera[0].GetComponent<AntialiasingAsPostEffect>().enabled = true;
        //arrayCamera[0].GetComponent<ColorCorrectionCurves>().enabled = true;

    }

    #endregion

    #region Metodos de criacao

    void CriarNaves()
	{
		arrayNaves = new GameObject[2];

        NaveAlemao.transform.position = Tabuleiro.GetComponent<Tabuleiro>().IdPosicao(new Vector3(5, 1, 5)) + new Vector3(0, 2, 0);
		
		NaveFrances.transform.position =  Tabuleiro.GetComponent<Tabuleiro>().IdPosicao(Tabuleiro.GetComponent<Tabuleiro>().DimensaoTabuleiro - new Vector3(1, 2, 1)) + new Vector3(0, 2, 0);

		arrayNaves[0] = NaveAlemao;
		arrayNaves[1] = NaveFrances;
		
		foreach(GameObject nave in arrayNaves)
		{
			nave.transform.parent = gameObject.transform;
			nave.GetComponent<Nave>().TerminouMover+= NaveTerminouMover;
		}

        NaveAlemao.GetComponent<Nave>()._idTileAtual = new Vector3(5, 1, 5);
		NaveFrances.GetComponent<Nave>()._idTileAtual = Tabuleiro.GetComponent<Tabuleiro>().DimensaoTabuleiro-new Vector3(1,2,1);

        Tabuleiro.GetComponent<Tabuleiro>().GetCasa(NaveAlemao.GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().jogadorAtual = NaveAlemao;
        Tabuleiro.GetComponent<Tabuleiro>().GetCasa(NaveFrances.GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().jogadorAtual = NaveFrances;

      

	
	}
	
	void CriarEventosCasa()
	{
		foreach(GameObject casa in Tabuleiro.GetComponent<Tabuleiro>().arrayCasas)
		{
			casa.GetComponent<Casa>().ClickCasa+= EventoClickCasa;
			casa.GetComponent<Casa>().OverCasa+= EventoOverCasa;
			casa.GetComponent<Casa>().ExitCasa+= EventoExitCasa;
		}
	}

    void CriarCamera()
    {
		arrayCamera = new GameObject[1];
		
		arrayCamera[0] = CameraJogo;
        arrayCamera[0].GetComponent<CamJogo>().targetJogador = arrayNaves[jogadorAtual].transform;

    }
	
	
	#endregion
	
	#region Eventos Casa

	public void EventoClickCasa (GameObject CasaObject)
	{


		if(CasaObject.GetComponent<Casa>().interacaoDisponivel)
		{
            Tabuleiro.GetComponent<Tabuleiro>().GetCasa(arrayNaves[jogadorAtual].GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().jogadorAtual = null;
			arrayNaves[jogadorAtual].gameObject.GetComponent<Nave>().MoverMesmoAndar(CasaObject.gameObject.transform.position,CasaObject.GetComponent<Casa>().ID);
			DesmarcarCasasDisponiveisMover();
			
		}

      
	
	}
    public void EventoOverCasa(GameObject CasaObject)
	{

		if(estadoJogada == EstadoJogada.EscolhendoCasa && CasaObject.GetComponent<Casa>().interacaoDisponivel==true)
		{
			CasaObject.GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.CasaSelecionada);
	
		}
		
	}
    public void EventoExitCasa(GameObject CasaObject)
	{
		if(estadoJogada == EstadoJogada.EscolhendoCasa && CasaObject.GetComponent<Casa>().interacaoDisponivel==true)
		{
			CasaObject.GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.CasaDisponivel);
			
			MarcarCasasJogadores();
		}
	}

	#endregion
	
	#region Metodos Casa
	
	void MarcarCasasDisponiveisMover(int numeroCasas)
	{
        Tabuleiro.GetComponent<Tabuleiro>().MarcarCasasDisponiveisMover(numeroCasas, arrayNaves[jogadorAtual].GetComponent<Nave>()._idTileAtual, arrayNaves[jogadorPosterior].GetComponent<Nave>()._idTileAtual);
	}
	
	void DesmarcarCasasDisponiveisMover()
	{
        Tabuleiro.GetComponent<Tabuleiro>().DesmarcarCasasDisponiveisMover(); 
	}
	
	void MarcarCasasJogadores()
	{
		for(int i = 0; i<arrayNaves.Length; i++)
		{
			if(i==0)
			Tabuleiro.GetComponent<Tabuleiro>().GetCasa(arrayNaves[i].GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.Nave1);
			else
			Tabuleiro.GetComponent<Tabuleiro>().GetCasa(arrayNaves[i].GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.Nave2);
		}
	}
	
	#endregion
	
	#region MetodosTabuleiro

	void VisibilidadeAndar(int andar)
	{
		Tabuleiro.GetComponent<Tabuleiro>().VisibilidadeAndar(andar);
	}
	
	void VisibilidadeTabuleiro(bool ativo)
	{	
		 Tabuleiro.GetComponent<Tabuleiro>().TabuleiroVisivel(ativo);
	}

    void InterageTabuleiro(bool ativo)
    {
        Tabuleiro.GetComponent<Tabuleiro>().TabuleiroAtivo(ativo);
    }


	#endregion
	
	void NaveTerminouMover (GameObject NaveObject)
	{
        Tabuleiro.GetComponent<Tabuleiro>().GetCasa(NaveObject.GetComponent<Nave>()._idTileAtual).GetComponent<Casa>().jogadorAtual = NaveObject;

		jogadorAtual++;
		jogadorPosterior--;
		
		if(jogadorAtual>1)
		{
			jogadorPosterior=1;
			jogadorAtual=0;
		}
		
		MarcarCasasDisponiveisMover(quantCasas);
		MarcarCasasJogadores();
        VisibilidadeAndar(andarAtual);
        arrayCamera[0].GetComponent<CamJogo>().MudarDeEstado(EstadoCamera.Padrao, arrayNaves[jogadorAtual].transform);
	}
	
	void TargetCameraCalculo()
	{
		float pos = (Tabuleiro.GetComponent<Tabuleiro>().DimensaoTabuleiro.x * Tabuleiro.GetComponent<Tabuleiro>().TamanhoCasa/2) - 10;
		
		TargetCamera.transform.position = new Vector3(pos,0,pos);
			
	}
	
	void Update () {
		
	}
	
	
	void Estadojogo(EstadoJogo estado)
	{
		if(estado == EstadoJogo.FocoJogador)
		{
			
            arrayCamera[0].GetComponent<CamJogo>().MudarDeEstado(EstadoCamera.FocadoJogador,arrayNaves[jogadorAtual].transform);
            StartCoroutine(Rotina2(1f));
		
		}

		else if(estado == EstadoJogo.Jogo)
		{

            arrayCamera[0].GetComponent<CamJogo>().MudarDeEstado(EstadoCamera.Padrao, arrayNaves[jogadorAtual].transform);
            StartCoroutine(Rotina1(2f));


		}

        estadoJogo = estado;
	}
	
	void ControlesApresentacao()
	{
		
	} 
}

	
	


	
	
