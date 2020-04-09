using UnityEngine;
using System.Collections;

public class Gerador : MonoBehaviour
{

    #region Atributos

    //GameObject[]posicoesIniciais;
	public GameObject intancia;

	float contador;
    public bool onda;
    int numeroAranhasParaGerar;
    float intervaloInstancia;
    int numeroInstanciasPorInstanciada;
    float intervaloEntreOndas;
    int nivelAtual;

    #endregion


    void Start()
	{
        //posicoesIniciais = GameObject.FindGameObjectsWithTag("EtzinhoPosition");
        onda = false;
        intervaloEntreOndas = 0;
        contador = 0;
	}

	
	void Update () 
	{

        if (onda && intervaloEntreOndas <= 0)
        {
            contador += Time.deltaTime;

            //checa se está na hora de instanciar
            if (contador <= intervaloInstancia)
            {
                //antes de tudo zera o contador
                contador = 0;

                //checar se a quantidade para instanciar por Instanciada é menor do que a quantidade que falta para instanciar
                if (numeroInstanciasPorInstanciada < numeroAranhasParaGerar)
                {
                    //instancia o numero de instancias pré-definido
                    for (int i = 0; i < numeroInstanciasPorInstanciada; i++)
                    {
                       // Vector3 posicao = posicoesIniciais[Random.Range(0, posicoesIniciais.GetLength(0))].transform.position;
                        GameObject instancia = (GameObject)Instantiate(intancia, new Vector3(1000,1000,1000), new Quaternion(0,0,0,0));
                        instancia.GetComponent<AndarRandom>().CriarCaminho();                        
                        numeroAranhasParaGerar--;
                    }
                    if (numeroAranhasParaGerar <= 0)
                    {
                        onda = false;
                        intervaloEntreOndas = nivelAtual + 2;
                    }
                }
                else
                {
                    //instancia o resto
                    for (int i = 0; i < numeroAranhasParaGerar; i++)
                    {
                        //Vector3 posicao = posicoesIniciais[Random.Range(0, posicoesIniciais.GetLength(0))].transform.position;
                        GameObject instancia = (GameObject)Instantiate(intancia, new Vector3(1000, 1000, 1000), new Quaternion(0, 0, 0, 0));
                        instancia.GetComponent<AndarRandom>().CriarCaminho();
                        numeroAranhasParaGerar--;
                    }
                    //para de usar o método
                    onda = false;
                    intervaloEntreOndas = nivelAtual + 2;
                }
            }
        }
        else
        {
            intervaloEntreOndas -= Time.deltaTime;
        }

	}


    public void InicarOnda(int _nivel)
    {
        //iniciar onda
        onda = true;

        //a quantidade de aranhas que aparece depende do nivel
        numeroAranhasParaGerar = _nivel;

        //mandar a quantidade de aranhas para o Game1
        Fase01.quantidadeAranhasOnda = numeroAranhasParaGerar;

        //o intervalo das instanciadas depende do nível
        intervaloInstancia = 100;

        //numero de instancias por instanciadas
        numeroInstanciasPorInstanciada = 1;

        nivelAtual = _nivel;
        }	
}


public class RandomSinal
{
	public static int Sortear()
	{
		var result = Random.Range(0,2);
		if(result == 1)
			return 1;
		else
			return -1;
	}
}
