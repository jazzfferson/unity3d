using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Jogador
{
    [SerializeField]
    private string m_nome;
    public string Nome { get => m_nome; private set => m_nome = value; }

    [SerializeField]
    private Pontuacao m_Pontuacao;
    public Pontuacao Pontuacao { get => m_Pontuacao; }

    public Jogador(string nome)
    {
        m_nome = nome;
        m_Pontuacao = new Pontuacao();
    }

    public bool IsEndedGame()
    {
        return m_Pontuacao.CurrentFrameIndex >= 9 && m_Pontuacao.FrameAtual.VerificarSeRodadaAcabou();   
    }
}
