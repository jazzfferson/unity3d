using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FrameBoliche
{
    public delegate void FrameBolicheDelegateHandler(FrameBoliche frameBoliche);
    public event FrameBolicheDelegateHandler OnRecebeuBonus;
    protected StaticMembers.TiposDeJogadas tipoJogada;
    public StaticMembers.TiposDeJogadas TipoJogada
    {
        get => tipoJogada;
    }
    public int FrameIndex { get => frameIndex;}

    public int jogadaAtual = 0;
    public int[] pinosDerrubados;
    [SerializeField]
    protected int pinosDerrubadosBonus;
    [SerializeField]
    protected int vezesParaReceberBonus = 0;
    [SerializeField]
    protected bool m_deveReceberBonus = false;
    [SerializeField]
    private bool m_iniciado = false;
    public bool DeveReceberBonus
    {
        get => m_deveReceberBonus;
    }
    public bool Iniciado
    {
        get => m_iniciado;
    }
    protected int frameIndex;

    public virtual bool VerificarSeRodadaAcabou()
    {
        return false;
    }
    public virtual StaticMembers.TiposDeJogadas ReceberPinosDerrubados(int quantidadePinosDerrubados)
    {
        m_iniciado = true;
        return tipoJogada;
    }

    public virtual void ReceberPinosBonus(int quantidadePinosBonusDerrubados)
    {
        if(m_deveReceberBonus)
        {
            Debug.Log("Frame" + TipoJogada + " index: " + FrameIndex + " recebeu: " + quantidadePinosBonusDerrubados);
           
            pinosDerrubadosBonus += quantidadePinosBonusDerrubados;

            vezesParaReceberBonus--;

            if(vezesParaReceberBonus<=0)
            {
                m_deveReceberBonus = false;
            }

            if (OnRecebeuBonus != null)
                OnRecebeuBonus(this);

        }
    }
    public virtual int TotalPinosDerrubados()
    {
        int totalPinosDerrubados = 0;

        foreach (int quantidade in pinosDerrubados)
        {
            totalPinosDerrubados += quantidade;
        }

        return totalPinosDerrubados;
    }
    public virtual int TotalPinosBonus()
    {
        return pinosDerrubadosBonus;
    }
    public virtual int TotalPontos()
    {
        return TotalPinosDerrubados() + TotalPinosBonus();
    }

}

public class FrameBolicheNormal: FrameBoliche
{
    public FrameBolicheNormal(int index)
    {
        frameIndex = index;
        pinosDerrubados = new int[2];
       // pinosDerrubadosBonus = new int[2];
        jogadaAtual = 0;
        tipoJogada = StaticMembers.TiposDeJogadas.Normal;
    }

    public override bool VerificarSeRodadaAcabou()
    {
        if (jogadaAtual == 1 || tipoJogada == StaticMembers.TiposDeJogadas.Strike)
        {
            return true;
        }
        else
            return false;
    }

     public override StaticMembers.TiposDeJogadas ReceberPinosDerrubados(int quantidadePinosDerrubados)
     {
         pinosDerrubados[jogadaAtual] = quantidadePinosDerrubados;

         if (jogadaAtual == 0 && quantidadePinosDerrubados == 10)
         {
             tipoJogada = StaticMembers.TiposDeJogadas.Strike;
             vezesParaReceberBonus = 2;
             m_deveReceberBonus = true;
         }
         else if (jogadaAtual == 1 && (pinosDerrubados[0] + quantidadePinosDerrubados) == 10)
         {
            tipoJogada = StaticMembers.TiposDeJogadas.Spare;
            vezesParaReceberBonus = 1;
            m_deveReceberBonus = true;
        }
         else
         {
             tipoJogada = StaticMembers.TiposDeJogadas.Normal;
         }
        return base.ReceberPinosDerrubados(quantidadePinosDerrubados);
     }

}
public class FrameBolicheUltimo : FrameBoliche
{
    int numeroMaximoDeJogadas = 1;

    public FrameBolicheUltimo(int index)
    {
        frameIndex = index;
        pinosDerrubados = new int[3];
        jogadaAtual = 0;
        tipoJogada = StaticMembers.TiposDeJogadas.Normal;
       // pinosDerrubadosBonus = new int[2];
    }

    public override bool VerificarSeRodadaAcabou()
    {

        if (jogadaAtual >= numeroMaximoDeJogadas)
        {
            return true;
        }
        else
            return false;
    }

    public override StaticMembers.TiposDeJogadas ReceberPinosDerrubados(int quantidadePinosDerrubados)
    {
        Debug.Log(jogadaAtual);
        pinosDerrubados[jogadaAtual] = quantidadePinosDerrubados;

        if (quantidadePinosDerrubados == 10)
        {
            tipoJogada = StaticMembers.TiposDeJogadas.Strike;
            numeroMaximoDeJogadas=2;
        }
        else if (jogadaAtual == 1 && (pinosDerrubados[0] + quantidadePinosDerrubados) == 10)
        {
            tipoJogada = StaticMembers.TiposDeJogadas.Spare;
            numeroMaximoDeJogadas=2;
        }
        else
        {
            tipoJogada = StaticMembers.TiposDeJogadas.Normal;
        }
        return base.ReceberPinosDerrubados(quantidadePinosDerrubados);
    }
}