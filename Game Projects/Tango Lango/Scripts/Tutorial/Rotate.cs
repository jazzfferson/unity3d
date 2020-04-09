using UnityEngine;
using System.Collections;
public enum Eixo { X, Y, Z };
public class Rotate : MonoBehaviour {

    
    public Eixo EixoDeRotacao;
    public float velocidade;

	void Start () {
	
	}
	void Update () {

        RotateUpdate();
	
	}
    void RotateUpdate()
    {
        switch (EixoDeRotacao)
        {
            case Eixo.X:
                transform.Rotate(velocidade * Time.deltaTime, 0, 0);
                break;
            case Eixo.Y:
                transform.Rotate(0, velocidade * Time.deltaTime, 0);
                break;
            case Eixo.Z:
                transform.Rotate(0, 0, velocidade * Time.deltaTime);
                break;
        }
    }
}
