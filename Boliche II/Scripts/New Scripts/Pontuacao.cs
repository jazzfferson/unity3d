using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Pontuacao
{
    public delegate void PontuacaoDelegateHandler(FrameBoliche frameQueRecebeuBonus);
    public event PontuacaoDelegateHandler OnRecebeuBonusEvent;

    public FrameBoliche[] JogadorFrames { get => m_jogadorFrames; }
    public FrameBoliche FrameAtual { get => m_jogadorFrames[CurrentFrameIndex]; }
    public int CurrentFrameIndex { get => m_currentFrameIndex;}

    [SerializeField]
    private FrameBoliche[] m_jogadorFrames;

    [SerializeField]
    private List<FrameBoliche> m_listaFramesFinalizados;
    public List<FrameBoliche> JogadorFramesFinalizados { get => m_listaFramesFinalizados; }

    [SerializeField]
    private int m_currentFrameIndex = 0;

    public Pontuacao()
    {
        m_jogadorFrames = new FrameBoliche[10];

        for (int i = 0; i < 9; i++)
        {
            m_jogadorFrames[i] = new FrameBolicheNormal(i);
            m_jogadorFrames[i].OnRecebeuBonus += Pontuacao_OnRecebeuBonus;     
        }

        m_jogadorFrames[9] = new FrameBolicheUltimo(9);
        m_jogadorFrames[9].OnRecebeuBonus += Pontuacao_OnRecebeuBonus;

        m_listaFramesFinalizados = new List<FrameBoliche>();
    }

    private void Pontuacao_OnRecebeuBonus(FrameBoliche frameBoliche)
    {
        if (OnRecebeuBonusEvent != null)
        {
            OnRecebeuBonusEvent(frameBoliche);
        }
    }

    public void SetFrame(int index)
    {
        if (index < 0 || index > 9) { Debug.Log("Set Frame: Index Incorreto"); return; }
        m_currentFrameIndex = index;
    }

    public void AdicionarFrameFinalizado(FrameBoliche frameFinalizado)
    {
        if (frameFinalizado.GetType() != typeof(FrameBolicheUltimo))
            m_listaFramesFinalizados.Add(frameFinalizado);
    }

    public void CalcularBonus(int numeroDePinosDerrubados)
    {

        m_listaFramesFinalizados.ForEach(frameFinalizado => 
        {
            frameFinalizado.ReceberPinosBonus(numeroDePinosDerrubados);
        });
    }

    public int TotalPoints()
    {
        int sum = 0;

        foreach (FrameBoliche frame in m_jogadorFrames)
        {
            sum += frame.TotalPontos();
        }

        return sum;
    }

} 
