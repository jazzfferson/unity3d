using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacarUI : MonoBehaviour
{
    public RectTransform rootTransform;
    public FrameUI[] frameUiArray;
    public CanvasGroup m_CanvasGroup;
    /* public Text total;*/
    public Text playerName;
    private int m_currentIndex = -1;
    private RectTransform m_RectTransform;
    
    private float m_TotalPointsValue;

    private void Awake()
    {
        for (int i= 0; i < frameUiArray.Length; i++)
        {
            frameUiArray[i].InitializeFrame(i);
        }
        //m_CanvasGroup = GetComponent<CanvasGroup>();
        m_RectTransform = GetComponent<RectTransform>();
        //total.text = "0";
    }

    public void Visibilty(bool show, float masterDelayMultiplier = 1)
    {
        float masterDelay = 2 * masterDelayMultiplier;

        if(show)
        {
            rootTransform.DOAnchorPosY(0, 0.5f).SetDelay(masterDelay + 0.5f);
        }
        else
        {
            float size = rootTransform.rect.height;
            rootTransform.DOAnchorPosY(size, 0.25f).SetDelay(masterDelay);
        }
       
    }

    public void SetPlayerName(Jogador jogador)
    {
        playerName.text = jogador.Nome;

    }

    public void PlacarSize(float scale)
    {
        float time = 0.5f;
        m_RectTransform.DOScale(new Vector3(scale, scale,1), time).SetEase(Ease.OutQuad);
        m_CanvasGroup.DOFade(scale, time).SetEase(Ease.OutQuad);
    }

    public void ShowCurrentFrame(int index)
    {
        if (index == m_currentIndex)
            return;

        if (index>0)
        {
            frameUiArray[index].rectTransform.anchoredPosition = frameUiArray[index - 1].rectTransform.anchoredPosition;
        }

        frameUiArray[index].Show();
        frameUiArray[index].Visibility(1);
        m_currentIndex = index;
    }

    public void UpdateResult(FrameBoliche frame,Jogador jogadorAtual)
    {
        if (frame.GetType() == typeof(FrameBolicheUltimo) && frame.Iniciado)
        {
            int jogadaAtual = frame.jogadaAtual;

            if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Normal)
            {               
                frameUiArray[frame.FrameIndex].SetLance(jogadaAtual, frame.pinosDerrubados[jogadaAtual]);
            }
            else if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Spare)
            {
                frameUiArray[frame.FrameIndex].SetLance(jogadaAtual, "/");
            }
            else
            {
                frameUiArray[frame.FrameIndex].SetLance(jogadaAtual, "X");
            }

            frameUiArray[frame.FrameIndex].ResultadoParcial(frame.TotalPontos());

        }
        else
        {
            if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Normal)
            {
                int jogadaAtual = frame.jogadaAtual;
                frameUiArray[frame.FrameIndex].SetLance(jogadaAtual, frame.pinosDerrubados[jogadaAtual]);
            }
            else if (frame.TipoJogada == StaticMembers.TiposDeJogadas.Spare)
            {
                frameUiArray[frame.FrameIndex].SetSpare(frame.pinosDerrubados[0]);
            }
            else
            {
                frameUiArray[frame.FrameIndex].SetStrike();
            }

            int pontosAcumulados = 0;

            foreach (FrameBoliche frameFinalizado in jogadorAtual.Pontuacao.JogadorFramesFinalizados)
            {
                pontosAcumulados += frameFinalizado.TotalPontos();
            }

            frameUiArray[frame.FrameIndex].ResultadoParcial(pontosAcumulados);
           // frameUiArray[frame.FrameIndex].ResultadoParcial(frame.TotalPontos());
        }

    }
   
   /*public void UpdateTotalPoints(Pontuacao pontuacao)
    {
        DOVirtual.Float(m_TotalPointsValue, pontuacao.TotalPoints(), 0.5f, (float valor) =>
        {
            m_TotalPointsValue = valor;
            total.text = Mathf.RoundToInt(m_TotalPointsValue).ToString();
        });
    }*/
}
