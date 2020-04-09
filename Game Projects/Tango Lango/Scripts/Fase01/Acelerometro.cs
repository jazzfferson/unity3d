using UnityEngine;
using System.Collections;

public class Acelerometro : MonoBehaviour 
{

    public static Vector3 Valor;
    	
	void Update () 
    {
        Acelerometro.Valor = Input.acceleration;
	}
}
