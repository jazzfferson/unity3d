using UnityEngine;
using System.Collections;

public class Tabuleiro : MonoBehaviour {
	
	public Win winPanel;
	public UILabel moves;
	public UILabel level;

	
   	public int Moves;

	public GameObject casa;
	private int tamanhoTabuleiro = 5;
	public float tamanhoCasa;
    	public float espacamentoCasa;
	private GameObject[,] arrayCasas;
    	public Transform casasRoot;
    	public UISprite bg;
   	public Color corJogada;
	
	
	IEnumerator Start () {



		Proprietes.estadoJogo = EstadoJogo.Jogando;
		Proprietes.jogadasEfetuadas = 0;
		level.text = (Fases.faseAtual+1).ToString();
		Proprietes.casasAtivas = 0;
		yield return new WaitForSeconds(0.1f);
		
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
                arrayCasas[i, j] = Instantiate(casa, new Vector3(casasRoot.position.x + (i * espacamentoCasa), casasRoot.position.y, casasRoot.position.z + (j  * espacamentoCasa)), Quaternion.identity) as GameObject;
                arrayCasas[i, j].transform.parent = casasRoot;
                arrayCasas[i, j].transform.localRotation = new Quaternion(0, 0, 0, 0);
                arrayCasas[i, j].transform.localScale = new Vector3(tamanhoCasa, tamanhoCasa, tamanhoCasa);
                arrayCasas[i, j].GetComponent<Casa>().Clickado += new Casa.DelegateCasa(Tabuleiro_Clickado);
				 arrayCasas[i, j].GetComponent<Casa>().Win+= new Casa.DelegateCasa(Tabuleiro_Win);

                foreach (CasaAtiva ativa in Fases.leveis[Fases.faseAtual].CasasAtivas)
				{
					if(i == ativa._i && j == ativa._j)
					{
						arrayCasas[i, j].GetComponent<Casa>().Ativa();
					}
				}
				

			}
		}		
	}
        void Tabuleiro_Clickado()
	    {
	        Moves++;
		moves.text = Moves.ToString();
		Instanciador.instancia.PlaySfx(0,1,1);
	    }
	void Tabuleiro_Win()
    	{
		
		
	Proprietes.estadoJogo = EstadoJogo.WinScreen;

		Proprietes.canClick = false;

        if (Proprietes.jogadasEfetuadas <= Fases.leveis[Fases.faseAtual].jogadasMinimas)
		{
            Fases.leveis[Fases.faseAtual].Score = 3;
		}
            else if (Proprietes.jogadasEfetuadas >= Fases.leveis[Fases.faseAtual].jogadasMinimas && Proprietes.jogadasEfetuadas <= Fases.leveis[Fases.faseAtual].jogadasMinimas + 2)
		{
            Fases.leveis[Fases.faseAtual].Score = 2;
		}
            else if (Proprietes.jogadasEfetuadas >= Fases.leveis[Fases.faseAtual].jogadasMinimas + 2 && Proprietes.jogadasEfetuadas <= Fases.leveis[Fases.faseAtual].jogadasMinimas + 4)
		{
            Fases.leveis[Fases.faseAtual].Score = 1;
		}
		else
		{
            Fases.leveis[Fases.faseAtual].Score = 0;
		}
		
		if(Fases.faseAtual+2>Fases.FasesDestravadas)
		{
			Fases.FasesDestravadas++;
			winPanel.Play(true);
		}
		else
		{
			winPanel.Play(false);
		}
		
		Instanciador.instancia.PlaySfx(2,0.5f,1);
		
		Invoke("DesabilitaClickCasa",0.5f);
		
        }
	void DesabilitaClickCasa()
	{
		Proprietes.canClick = false;
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
	void Retry()
	{
	   Instanciador.instancia.PlaySfx(3,0.4f,1);
	   LoadScreenManager.instance.LoadScreenWithFadeInOut("Joguinho",0.3f);
	}
   
	
}
