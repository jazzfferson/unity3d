using UnityEngine;
using System.Collections;

public class Tabuleiro : MonoBehaviour {
	
	public static int casasAtivas;
	public GameObject casa;
	private int tamanhoTabuleiro = 5;
	private float tamanhoCasa = 1.2f;
	private GameObject[,] arrayCasas;
	
	void Start () {
	
		MontarCasas();
		AcharCasas();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void MontarCasas()
	{
		arrayCasas = new GameObject[tamanhoTabuleiro,tamanhoTabuleiro];
		
		for(int i = 0; i < tamanhoTabuleiro; i ++)
		{
			for(int j = 0; j < tamanhoTabuleiro; j ++)
			{
				arrayCasas[i,j] = Instantiate(casa,new Vector3(i*tamanhoCasa,0,j*tamanhoCasa),Quaternion.identity) as GameObject;			
			}
		}
		
		arrayCasas[2,2].GetComponent<Casa>().Ativa();
			
	}
	void AcharCasas()
	{
		
		Casa casaAtual;
		
		for(int i = 0; i < tamanhoTabuleiro; i ++)
		{
			for(int j = 0; j < tamanhoTabuleiro; j ++)
			{
				casaAtual = arrayCasas[i,j].GetComponent<Casa>();
				
				
				
				//Checar casa superior
				
				if((j-1)>=0 && ((j-1) <tamanhoTabuleiro))
				{
					casaAtual.casaSuperior = arrayCasas[i,j-1].GetComponent<Casa>();
				}
				else
				{
					casaAtual.casaSuperior = null;
				}
				
				if((j+1)>=0 && (j+1) <tamanhoTabuleiro)
				{
					casaAtual.casaInferior = arrayCasas[i,j+1].GetComponent<Casa>();
				}
				else
				{
					casaAtual.casaInferior = null;
				}
				
				if((i-1)>=0 && (i-1) <tamanhoTabuleiro)
				{
					casaAtual.casaEsquerda = arrayCasas[i-1,j].GetComponent<Casa>();
				}
				else
				{
					casaAtual.casaEsquerda = null;
				}
				
				if((i+1)>=0 && (i+1) <tamanhoTabuleiro)
				{
					casaAtual.CasaDireita = arrayCasas[i+1,j].GetComponent<Casa>();
				}
				else
				{
					casaAtual.CasaDireita = null;
				}
		
				
				
				
			}
		}
		
	}
	
}
