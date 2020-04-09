using UnityEngine;
using System.Collections;

public class Frame {
	public int[] pinosDerrubados;
    public int jogadaAtual;
	public StaticMembers.TiposDeJogadas tipoJogada;
	public bool canChange;
	
    public Frame()
    {
    }
	
	public virtual bool VerificarJogada()
	{
		return false;
	}
	
	public virtual void ReceberQntPinos(int qntPinos)
	{
	}
}
