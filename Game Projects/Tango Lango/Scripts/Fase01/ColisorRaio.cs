using UnityEngine;
using System.Collections;

public class ColisorRaio : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.name == "ColisorPersonagem")
        {
            GameData.numVidas-= 1f;
            Pontuacao.instance.AtualizarPontuacao();
            //Handheld.Vibrate();
        }
    }
}
