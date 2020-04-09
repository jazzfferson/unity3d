using UnityEngine;
using System.Collections;

public class Fase02 : MonoBehaviour 
{
    public UILabel TextoTimer;
    public UILabel TextoVidas;
    public GameObject SoundController;
	bool next = true;
	void Start () 
    {
        //começar a tocar a musica da fase 2
       // SoundController.instance.PlayMusic(1, 1);
                
        //atualizar e capiturar as informações do GameData
        GameData.faseAtual = 2;
        SoundController.GetComponent<SoundController>().PlayMusic(1, 1);
	}
	

	void Update ()
    {
        #region BACK do celular

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu", 2f);
			//Application.LoadLevel("Menu");
        }

        #endregion

        GameData.tempoFicouVivo += Time.deltaTime;
        //imprimir no Painel
        TextoTimer.text = string.Format("{0:0.0}", GameData.tempoFicouVivo) + " sec";
        TextoVidas.text = ((int)GameData.numVidas).ToString();


        if (GameData.numVidas < 0 && next)
        {
            FimJogo(); 
			next = false;
        }
	}


    void FimJogo()
    {
        //salvar recorde
        GameData.NovoRecorde(GameData.tempoFicouVivo, true);
        //salvar ultima pontuacao para saber qual deve ser a ultima tela, GameOver ou Novo Recorde
        GameData.ultimaPontucao = GameData.tempoFicouVivo;
        //ir para proxima tela
		//Application.LoadLevel("GameOver");		
        LoadScreenManager.instance.LoadScreenWithFadeInOut("GameOver", 0.8f);
    }
}
