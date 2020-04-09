using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultsUI : MonoBehaviour
{

    public TextMeshProUGUI jogador1Name;
    public List<FrameUI> jogador1Results;

    public TextMeshProUGUI jogador2Name;
    public List<FrameUI> jogador2Results;

    public Jogador jogador1Test, jogador2Test;


    private CanvasGroup m_CanvasGroup;

    // Start is called before the first frame update
    void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;


        jogador1Test = new Jogador("Jazzfferson");
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(3);
        jogador1Test.Pontuacao.CalcularBonus(3);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(5);       
        jogador1Test.Pontuacao.CalcularBonus(5);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);

        jogador1Test.Pontuacao.SetFrame(1);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(2);
        jogador1Test.Pontuacao.CalcularBonus(2);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(8); 
        jogador1Test.Pontuacao.CalcularBonus(8);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(2);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(10);
        jogador1Test.Pontuacao.CalcularBonus(10);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);

        jogador1Test.Pontuacao.SetFrame(3);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(6);
        jogador1Test.Pontuacao.CalcularBonus(6);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(3);       
        jogador1Test.Pontuacao.CalcularBonus(3);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(4);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador1Test.Pontuacao.CalcularBonus(1);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(5);       
        jogador1Test.Pontuacao.CalcularBonus(5);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(5);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(8);
        jogador1Test.Pontuacao.CalcularBonus(8);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(2);     
        jogador1Test.Pontuacao.CalcularBonus(2);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(6);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(4);
        jogador1Test.Pontuacao.CalcularBonus(4);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(4);  
        jogador1Test.Pontuacao.CalcularBonus(4);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(7);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador1Test.Pontuacao.CalcularBonus(1);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(9);
        jogador1Test.Pontuacao.CalcularBonus(9);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(8);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador1Test.Pontuacao.CalcularBonus(1);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(9);
        jogador1Test.Pontuacao.CalcularBonus(9);
        jogador1Test.Pontuacao.AdicionarFrameFinalizado(jogador1Test.Pontuacao.FrameAtual);
        jogador1Test.Pontuacao.SetFrame(9);

        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador1Test.Pontuacao.CalcularBonus(1);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(9);
        jogador1Test.Pontuacao.CalcularBonus(9);
        jogador1Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador1Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(5);
        jogador1Test.Pontuacao.CalcularBonus(5);



        jogador2Test = new Jogador("Mariazinha");
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(7);
        jogador2Test.Pontuacao.CalcularBonus(7);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(3);
        jogador2Test.Pontuacao.CalcularBonus(3);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);

        jogador2Test.Pontuacao.SetFrame(1);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(5);
        jogador2Test.Pontuacao.CalcularBonus(5);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(4);
        jogador2Test.Pontuacao.CalcularBonus(4);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(2);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(4);
        jogador2Test.Pontuacao.CalcularBonus(4);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador2Test.Pontuacao.CalcularBonus(1);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);

        jogador2Test.Pontuacao.SetFrame(3);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(9);
        jogador2Test.Pontuacao.CalcularBonus(9);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador2Test.Pontuacao.CalcularBonus(1);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(4);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(10);
        jogador2Test.Pontuacao.CalcularBonus(10);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(5);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(10);
        jogador2Test.Pontuacao.CalcularBonus(10);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(6);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(6);
        jogador2Test.Pontuacao.CalcularBonus(6);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(4);
        jogador2Test.Pontuacao.CalcularBonus(4);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(7);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(3);
        jogador2Test.Pontuacao.CalcularBonus(3);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(2);
        jogador2Test.Pontuacao.CalcularBonus(2);
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(8);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(10);
        jogador2Test.Pontuacao.CalcularBonus(10);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.AdicionarFrameFinalizado(jogador2Test.Pontuacao.FrameAtual);
        jogador2Test.Pontuacao.SetFrame(9);

        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(10);
        jogador2Test.Pontuacao.CalcularBonus(10);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(1);
        jogador2Test.Pontuacao.CalcularBonus(1);
        jogador2Test.Pontuacao.FrameAtual.jogadaAtual++;
        jogador2Test.Pontuacao.FrameAtual.ReceberPinosDerrubados(7);
        jogador2Test.Pontuacao.CalcularBonus(7);


        for (int i = 0; i < jogador1Results.Count; i++)
        {
            jogador1Results[i].InitializeFrame(i);
           
        }
        for (int i = 0; i < jogador2Results.Count; i++)
        {
            jogador2Results[i].InitializeFrame(i);
        }

    }

   /* public void ShowResultsPlaceholder()
    {
        SetResults(jogador1Test, jogador2Test);
    }*/

    public void SetResults(Jogador jogador1, Jogador jogador2)
    {
        m_CanvasGroup.DOFade(1, 1).OnComplete(() =>
        {
            
            SetResult(jogador1, jogador1Results, jogador1Name);
            SetResult(jogador2, jogador2Results, jogador2Name);
        });
        
    }

    private void SetResult(Jogador jogador, List<FrameUI> framesResultList, TextMeshProUGUI nomeJogador)
    {
        nomeJogador.text = jogador.Nome;
        int totalPontosIterator = 0;

        for (int i = 0; i < jogador.Pontuacao.JogadorFrames.Length; i++)
        {
            framesResultList[i].Visibility(1, 0.5f, 0.1f);

            Debug.Log("Visibility On Complete ResultsUI");

            FrameBoliche frame = jogador.Pontuacao.JogadorFrames[i];
            if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Normal)
            {
                framesResultList[i].SetLance(0, frame.pinosDerrubados[0]);
                framesResultList[i].SetLance(1, frame.pinosDerrubados[1]);

                if (frame.GetType() == typeof(FrameBolicheUltimo))
                {
                    framesResultList[i].SetLance(2, frame.pinosDerrubados[2]);
                }
            }
            else if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Spare)
            {
                framesResultList[i].SetSpare(frame.pinosDerrubados[0]);
            }
            else
            {
                framesResultList[i].SetStrike();
            }

            totalPontosIterator += frame.TotalPontos();
            framesResultList[i].ResultadoParcial(totalPontosIterator);
        }
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
