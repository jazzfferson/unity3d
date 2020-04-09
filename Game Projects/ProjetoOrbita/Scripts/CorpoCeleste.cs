using UnityEngine;
using System.Collections;
public enum Eixo {X,Y,Z};
public class CorpoCeleste : MonoBehaviour {
	
	public float velocidade;
	public Eixo eixo = Eixo.Z;
	// Use this for initialization
	void Start () {
	
	}
	void Update () {
		
	Rotacao();
		
	}
	void Rotacao()
	{
		switch (eixo)
		{
			
			case Eixo.X:
				transform.Rotate(velocidade*Time.deltaTime,0,0);
				break;
			case Eixo.Y:
				transform.Rotate(0,velocidade*Time.deltaTime,0);
				break;
			case Eixo.Z:
				transform.Rotate(0,0,velocidade*Time.deltaTime);
				break;
			
		}
	}
}
