using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tabuleiro : MonoBehaviour {
	
public GameObject PrefabCasa;
public float TamanhoCasa;
public Vector3 DimensaoTabuleiro;
public float OffsetAltura;
	
public UIPanel[] panelAndar;

	

int andarAtual=1;


bool ativo=false;

	
	
[HideInInspector] public GameObject[,,] arrayCasas;
	
	void Start () {
		
		MontarTabuleiro();
	}
	
	void MontarTabuleiro()
	{
		
		//Trocar este metodo no futuro. Instanciar todas as casas faz com que o jogo trave
		//no inicio, logo apos carregar. Ao iniciar a cena com as casas ja instanciadas evita-se este
		//problema.
		
		arrayCasas = new GameObject [(int)DimensaoTabuleiro.x,(int)DimensaoTabuleiro.y,(int)DimensaoTabuleiro.z];
		
		for(int x = 0; x < DimensaoTabuleiro.x; x ++)
		{
			for(int y = 0; y < DimensaoTabuleiro.y; y ++)
			{
				for(int z = 0; z < DimensaoTabuleiro.z; z ++)
				{
					arrayCasas[x,y,z] = (GameObject)Instantiate(PrefabCasa,new Vector3(x*TamanhoCasa,y*TamanhoCasa*OffsetAltura,z*TamanhoCasa),Quaternion.Euler(270,0,0));
					arrayCasas[x,y,z].GetComponent<Casa>().ID = new Vector3(x,y,z);
					arrayCasas[x,y,z].gameObject.transform.parent = panelAndar[y].transform;
					
				} 
			}
		}
		
	}
	
	public Vector3 IdPosicao(Vector3 id)
	{
		return arrayCasas[(int)id.x,(int)id.y,(int)id.z].GetComponent<Casa>().transform.position;
	}
	
	public List<GameObject> GetCasasDisponiveis(Vector3 idNave,Vector3 idNaveInimiga,int distancia)
	{
		
		
		List<GameObject>casasDisp = new List<GameObject>();
		
		int eixoXmin = (int)Mathf.Clamp(idNave.x-distancia,0,DimensaoTabuleiro.x-1);
		int eixoYmin = (int)Mathf.Clamp(idNave.y-distancia,0,DimensaoTabuleiro.y-1);
		int eixoZmin = (int)Mathf.Clamp(idNave.z-distancia,0,DimensaoTabuleiro.z-1);
		
		int eixoXmax = (int)Mathf.Clamp(idNave.x+distancia,0,DimensaoTabuleiro.x-1);
		int eixoYmax = (int)Mathf.Clamp(idNave.y+distancia,0,DimensaoTabuleiro.y-1);
		int eixoZmax = (int)Mathf.Clamp(idNave.z+distancia,0,DimensaoTabuleiro.z-1);
		
		
		for(int x = eixoXmin; x <=eixoXmax; x ++)
		{
			for(int y = eixoYmin; y <=eixoYmax; y ++)
			{
				for(int z = eixoZmin; z <=eixoZmax; z ++)
				{
					if(idNaveInimiga.x == x && idNaveInimiga.y == y && idNaveInimiga.z ==z)
					{
					}
                    if (idNave.x == x && idNave.y == y && idNave.z == z)
                    {
                    }
					else
					{
						casasDisp.Add(arrayCasas[x,y,z]);
					}
				}
			}
		}
		
		return casasDisp;
		
		
	}

    public void TransparenciaCasas(Vector3 PosicaoNave,int quantidadeFade,int offsetFade)
    {
        float distancia = 0f;

        foreach (GameObject ca in arrayCasas)
        {
            distancia = Vector3.Distance(PosicaoNave,ca.transform.position);
            distancia = Mathf.Abs((distancia - offsetFade) / quantidadeFade);
            ca.GetComponent<Casa>().Alpha(ca.GetComponent<Casa>().casaColor.a - distancia, 1, iTween.EaseType.linear);
        }
    }
    
	public GameObject GetCasa(Vector3 id)
	{
		return arrayCasas[(int)id.x,(int)id.y,(int)id.z];
	}
	
	/// <summary>
	/// Define qual andar estará visivel para o jogador.Ao desligar o andar
	/// a checagem de colisão da casa também é desativada
	/// </summary>
	/// <param name='andar'> O andar que estará visivel
	/// </param>
	public void VisibilidadeAndar(int andar)
	{
		
		
		for(int x = 0; x < DimensaoTabuleiro.x; x ++)
		{
			for(int y = 0; y < DimensaoTabuleiro.y; y ++)
			{
				for(int z = 0; z < DimensaoTabuleiro.z; z ++)
				{
                    if (arrayCasas[x, y, z].GetComponent<Casa>().jogadorAtual==null)
                    {
                        if (y == andar)
                        {
             
							arrayCasas[x, y, z].GetComponent<Casa>().AndarAtual(true);
							panelAndar[y].gameObject.layer = 0;
							arrayCasas[x, y, z].layer = 0;

                        }
                        else
                        {
                         
							arrayCasas[x, y, z].GetComponent<Casa>().AndarAtual(false);
							panelAndar[y].gameObject.layer = 2;
							arrayCasas[x, y, z].layer = 2;
                        }
                    }
					else
					{
						 if (y == andar)
                        {
             
							panelAndar[y].gameObject.layer = 0;
							arrayCasas[x, y, z].layer = 0;

                        }
                        else
                        {
                         
							panelAndar[y].gameObject.layer = 2;
							arrayCasas[x, y, z].layer = 2;
                        }
						
					}
				}
			}
		}

        andarAtual = andar;
	}

    public void MarcarCasasDisponiveisMover(int numeroCasas,Vector3 idNave1,Vector3 idNave2)
    {
        foreach (GameObject casa in GetCasasDisponiveis(idNave1,idNave2,numeroCasas))
        {
            casa.GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.CasaDisponivel);
            casa.GetComponent<Casa>().interacaoDisponivel = true;
        }
    }

    public void DesmarcarCasasDisponiveisMover()
    {
        ResetarCasas();
        VisibilidadeAndar(andarAtual);
    }

    public void ResetarCasas()
    {
        foreach (GameObject casa in arrayCasas)
        {
            casa.GetComponent<Casa>().interacaoDisponivel = false;
            casa.GetComponent<Casa>().disponivelParaMover = false;
            casa.GetComponent<Casa>().TrocarEstadoCasa(EstadoCasa.CasaColor);
            
        }
    }
	
	public void TabuleiroAtivo(bool ativo)
	{
		foreach(GameObject casa in arrayCasas)
		{
			//casa.GetComponent<Casa>().CasaAtiva(ativo);	
		}

	}

    public void TabuleiroVisivel(bool visivel)
    {
        foreach (GameObject casa in arrayCasas)
        {
            //casa.GetComponent<Casa>().VisibilidadeCasa(visivel);
        }

    }
	
	public void Iteragivel(bool interage)
	{
		foreach(GameObject casa in arrayCasas)
		{
				
			casa.GetComponent<Casa>().interagivel=interage;
		}
	}
	
	
	
}
