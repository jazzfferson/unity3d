using UnityEngine;
using System.Collections;

public class Pontuacao : MonoBehaviour {

	public static Pontuacao instance;
	
	public UILabel pontucaoText;
	
	void Start()
	{
		if(instance==null)
		{
			instance = this;
		}
	}
	
	void Update()
	{
		
	}
	
	public void AtualizarPontuacao()
	{
	
            pontucaoText.text = Fase01.numAranhasMortas.ToString();
            Go.to(pontucaoText.transform, 0.1f, new GoTweenConfig().scale(new Vector3(.05f, .05f, 1), false).setEaseType(GoEaseType.ElasticInOut).setIterations(2, GoLoopType.PingPong));	
	}

}
