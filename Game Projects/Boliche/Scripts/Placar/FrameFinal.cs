using UnityEngine;
using System.Collections;

public class FrameFinal : Frame {
	bool ok;
	
	public FrameFinal()
	{
		pinosDerrubados = new int[2];
   		jogadaAtual = 0;
	}
	
	public override bool VerificarJogada()
    {
	if(ok)
	{
		ok = false;
		if(jogadaAtual == 1)
			{	
				canChange = true;
				return true;
			}
		else
			{
				jogadaAtual++;
				return false;
			}
	}
	else
		return false;
    }
	
	public override void ReceberQntPinos (int qntPinos)
	{
		if(pinosDerrubados[0] + qntPinos > 11)
		{
			return;
		}
		
		pinosDerrubados[jogadaAtual] = qntPinos;
		ok = true;
		if(jogadaAtual == 0 && qntPinos == 10)
		{
			tipoJogada = StaticMembers.TiposDeJogadas.Strike;
		}
		else if(jogadaAtual == 1 && pinosDerrubados[0] + qntPinos == 10)
		{
			tipoJogada = StaticMembers.TiposDeJogadas.Spare;
		}
		else
		{
			tipoJogada = StaticMembers.TiposDeJogadas.Normal;
		}
	}
}
