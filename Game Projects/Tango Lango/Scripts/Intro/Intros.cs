using UnityEngine;
using System.Collections;

public class Intros : MonoBehaviour
{

    public Material[] spritesTelas;
    public GameObject tela;
    public float intervaloEntreTelas;
    float contador;
    int spriteAtual;
	bool entra = true;

    void Start()
    {
        contador = intervaloEntreTelas;
        spriteAtual = 0;
        //aplicar o primeiro material
        tela.renderer.material = spritesTelas[spriteAtual];
    }


    void Update()
    {
        contador -= Time.deltaTime;

        if (contador <= 0 && entra)
        {
            //checar se foi exibido a ultima tela
            if (spriteAtual == spritesTelas.Length-1)
            {
               // Application.LoadLevel("Menu");
				entra = false;
				LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu",0.5f);
            }
            else
            {
                spriteAtual++;
                //aplicar proxima sprite a tela
                tela.renderer.material = spritesTelas[spriteAtual];
                //zerar contador
                contador = intervaloEntreTelas;
            }
        }
    }
}
