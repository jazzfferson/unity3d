using UnityEngine;
using System.Collections;

public class ContadorPinos : MonoBehaviour {



    void OnTriggerExit(Collider pino)
    {
        if (pino.gameObject.CompareTag("Pino"))
        {
            PinoCaiu(pino.gameObject);
        }
    }
    public delegate void EventHandler(GameObject pino);
    public event EventHandler PinoCaiu;
}
