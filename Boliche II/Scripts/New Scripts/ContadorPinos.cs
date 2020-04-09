using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContadorPinos : MonoBehaviour {


   
    public bool EnableContador { get => enableContador; set => enableContador = value; }
    private bool enableContador = true;

    public List<Pino> PinosDerrubados { get => m_pinosDerrubados;}
    private List<Pino> m_pinosDerrubados;

    public int NumeroDePinosDerrubados
    {
        get => m_pinosDerrubados.Count;
    }


    private void Awake()
    {
        m_pinosDerrubados = new List<Pino>();
    }
    public void ResetContador()
    {
        m_pinosDerrubados.Clear();
    }

    void OnTriggerExit(Collider pinoCollider)
    {
        if (!enableContador)
            return;
       
        if (pinoCollider.gameObject.CompareTag("Pino"))
        {
            Pino pino = pinoCollider.GetComponent<Pino>();

            if (pino == null)
                return;

            if (!pino.derrubado)
            {
                pino.derrubado = true;
                m_pinosDerrubados.Add(pino);

                if (OnPinoCaiu != null)
                    OnPinoCaiu(pino);
            }
        }
    }

    public delegate void EventHandler(Pino pino);
    public event EventHandler OnPinoCaiu;
}
