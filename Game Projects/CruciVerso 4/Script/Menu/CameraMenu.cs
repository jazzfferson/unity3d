using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour {
	
	public TrocaBolinha trocaBolinhaPagina;
	public Transform[] arrayCameraPosition;
	public float velocidadeTransicao;
	public int diferenca;
	
	
	
	float velocityTransition = 0.8f;
	GoEaseType type = GoEaseType.CubicInOut;
	int posicaoAtual = 0;
	
	
	Vector3 positionTo;
	int position;
	
	void Awake()
	{
	}
	void Start () {
		
		
		positionTo = transform.position;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.touchCount > 4 || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
		
		
		if(GetComponent<UIDraggableCamera>().mPressed)
		{
			switch (posicaoAtual)
			{
			case 0:
			if(transform.localPosition.x > diferenca)
				{
					position = 1;
					positionTo = arrayCameraPosition[position].position;
				}
			else if(transform.position.x!=arrayCameraPosition[0].position.x)
				{
					position = 0;
			
					positionTo = arrayCameraPosition[position].position;
				}
			break;
				
				case 1:
			if(transform.localPosition.x > arrayCameraPosition[1].localPosition.x+diferenca)
				{
					position = 2;

					positionTo = arrayCameraPosition[position].position;
				}
			else if(transform.localPosition.x < arrayCameraPosition[1].localPosition.x-diferenca )
				{
					position = 0 ;

					positionTo = arrayCameraPosition[position].position;
				}
			else if(transform.position.x!=arrayCameraPosition[1].position.x)
				{
					position= 1;

					positionTo = arrayCameraPosition[position].position;
				}
			break;
				
				case 2:
		
			if(transform.localPosition.x < arrayCameraPosition[2].localPosition.x-diferenca)
				{
					position = 1;
				
					positionTo = arrayCameraPosition[position].position;
				}
			else if(transform.position.x!=arrayCameraPosition[2].position.x)
				{
					position = 2;
			
					positionTo = arrayCameraPosition[position].position;
				}
			break;
			}
			
			
		}
		else if(!GetComponent<UIDraggableCamera>().mPressed)
		{
		
			posicaoAtual = position;
			if(posicaoAtual==0)
			{
				LogoChange.instance.ChangeLogo(true,0.3f);
			}
			else
			{
				LogoChange.instance.ChangeLogo(false,0.3f);
			}
			
			if(transform.position!=positionTo)
			{
				transform.position = Vector3.Lerp(transform.position,positionTo,Time.deltaTime*velocidadeTransicao);
			}
			
			
			if(Vector3.Distance(transform.position,positionTo)<0.4f)
			{
				trocaBolinhaPagina.SetBolinha(posicaoAtual);
				
			}
			
		}
		

	}
	
    void StartGame()
    {
        LoadScreenManager.instance.LoadScreenWithFadeInOut("Jogo-Exploration", 1f);
    }
	 void StartGame2()
    {
        LoadScreenManager.instance.LoadScreenWithFadeInOut("Jogo", 1f);
    }
}
