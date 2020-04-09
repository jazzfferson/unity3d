using UnityEngine;
using System.Collections;

public class Casa : MonoBehaviour {

	public GameObject Bolinha;
	public Casa casaSuperior,casaInferior,casaEsquerda,CasaDireita;
	bool ativa;
	
	void Start()
	{
		
	}
	public void Ativa()
	{
		if(ativa)
		{
			
			ativa = false;
			Bolinha.SetActive(false);
			Tabuleiro.casasAtivas--;
		}
		else
		{
			
			ativa = true;
			Bolinha.SetActive(true);
			Tabuleiro.casasAtivas++;
		}
		
		print(Tabuleiro.casasAtivas);
		
	}
	public void OnMouseDown()
	{
		Ativa();
		AtivarCasas();
		
		if(Tabuleiro.casasAtivas==0)
		{
			print("Ganhou");
			
		}
		
	}
	public void AtivarCasas()
	{
		if(casaSuperior!=null)
		{
			casaSuperior.Ativa();
		}
		if(casaInferior!=null)
		{
			casaInferior.Ativa();
		}
		if(casaEsquerda!=null)
		{
			casaEsquerda.Ativa();
		}
		if(CasaDireita!=null)
		{
			CasaDireita.Ativa();
		}
	}
	
}
