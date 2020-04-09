using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	
	public static Tutorial instance;
	
	public UISprite[] casasTabuleiroTutorial;
	public Transform[] handPositions;
	public UISprite handTutorial;
	public UILabel labelTutorial;
	public UIPanel tabuleiroPanel;
	Color invertedColor;
	int indexTutorial = 0;
	int[] indexsCasasAcessas;
	
	GoTween tween1;
	GoTween tween2;

	bool ultimaAnimacao;
	bool canClick = true;
	
	
	
	void Start()
	{
		if(instance ==null)
		{
			instance = this;
		}
		invertedColor = casasTabuleiroTutorial[0].color;		
	}
	
	void StartAnimation1()
	{
		Go.killAllTweensWithTarget(handTutorial.gameObject);
		AnimHand1(0.5f);
	}
	void AnimHand1(float time)
	{
		tween1 = Go.to(handTutorial.transform,time,new GoTweenConfig().position(new Vector3(0,0,0.2f),true).onComplete(Complete=>
		{
		 AnimHand2(time);	
		 InvertCasas();	
		}));
	}
	void AnimHand2(float time)
	{
		tween2 = Go.to(handTutorial.transform,time,new GoTweenConfig().position(new Vector3(0,0,-0.2f),true).onComplete(Complete=>
		{
		
			
		AnimHand1(time);	
			
		}));
	}
	
	void StopAnimation()
	{
	
	   if(tween1!=null)
	   tween1.destroy();
	   if(tween2!=null)
	   tween2.destroy();
	}
	
	bool inverterCasasAnima1;
	
	void InvertCasas()
	{
		
		if(!ultimaAnimacao)
		{
		if(inverterCasasAnima1)	
		{
			inverterCasasAnima1 = false;
			
			for(int i = 0 ; i <casasTabuleiroTutorial.Length; i++)
			{
				for(int j = 0 ; j <indexsCasasAcessas.Length; j++)
				{
					if(i == indexsCasasAcessas[j])
					{
						casasTabuleiroTutorial[i].color = invertedColor;
					}
				}
			}

			
			
		}
		else
		{
			inverterCasasAnima1 = true;
			
			for(int i = 0 ; i <casasTabuleiroTutorial.Length; i++)
			{
				for(int j = 0 ; j <indexsCasasAcessas.Length; j++)
				{
					if(i == indexsCasasAcessas[j])
					{
						casasTabuleiroTutorial[i].color = Color.white;
					}
				}
			}
		}
		}
		
		else
		{
			
			
			if(inverterCasasAnima1)	
			{
				inverterCasasAnima1 = false;
				casasTabuleiroTutorial[1].color = invertedColor;
				casasTabuleiroTutorial[3].color = invertedColor;
				casasTabuleiroTutorial[4].color = Color.white;
				casasTabuleiroTutorial[5].color = invertedColor;
				casasTabuleiroTutorial[7].color = invertedColor;
			}
			else
			{
				inverterCasasAnima1 = true;
				casasTabuleiroTutorial[1].color = Color.white;
				casasTabuleiroTutorial[3].color = Color.white;
				casasTabuleiroTutorial[4].color = invertedColor;
				casasTabuleiroTutorial[5].color = Color.white;
				casasTabuleiroTutorial[7].color = Color.white;
			}
			
		}
	}
	void ClearCasas()
	{
		
		foreach(UISprite casa in casasTabuleiroTutorial)
		{
			casa.color = invertedColor;
		}
	}	
	void NextTutorial()
	{
		
		if(!canClick)
			return;
		
		canClick = false;
		Invoke("CanClick",0.4f);
		
		Instanciador.instancia.PlaySfx(3, 0.4f, 1);
		
		
		
		if(indexTutorial<4)
		{
			indexTutorial++;	
		}
		else
		{
			indexTutorial = 0;
			NextTutorial();
			
		}
		
		SetTutorial();
		
		
		
	}
	void SetTutorial()
	{
		switch(indexTutorial)
		{
			
			case 0:
					
			TweenAlpha.Begin(tabuleiroPanel.gameObject,0.1f,0);
			TweenAlpha.Begin(labelTutorial.gameObject,0.1f,1).delay = 0.6f;
				
			break;
				
				
			case 1:
			inverterCasasAnima1 = false;
			ultimaAnimacao = false;
			StopAnimation();
			handTutorial.transform.position = handPositions[0].position;
			ClearCasas();
			indexsCasasAcessas = new int[]{1,3,4,5,7};
				
			TweenAlpha.Begin(labelTutorial.gameObject,0.1f,0);
			TweenAlpha.Begin(tabuleiroPanel.gameObject,0.1f,1).delay =0.6f;
				
			StartAnimation1();
			break;
				
			case 2:
			inverterCasasAnima1 = false;
			StopAnimation();
			handTutorial.transform.position = handPositions[1].position;
			ClearCasas();
			indexsCasasAcessas = new int[]{0,1,3};
			StartAnimation1();
				
			break;
				
				
			case 3:
			inverterCasasAnima1 = false;	
			StopAnimation();
			handTutorial.transform.position = handPositions[2].position;
			ClearCasas();
			indexsCasasAcessas = new int[]{2,4,5,8};
			StartAnimation1();
				
			break;
			
			case 4:
			
			inverterCasasAnima1 = false;
			ultimaAnimacao = true;
			StopAnimation();
			handTutorial.transform.position = handPositions[0].position;
			ClearCasas();
			casasTabuleiroTutorial[4].color = Color.white;
			indexsCasasAcessas = new int[]{4};
			StartAnimation1();
				
			break;
		}
		
	}
	public void Quit()
	{
	
		
	   if(!canClick)
			return;
		
		canClick = false;
		Invoke("CanClick",0.4f);
		
	   StopAnimation();
	   indexTutorial = 0;
	   SetTutorial();
	   
	   Menu.instance.MenuPrincipal();
	}
	void CanClick()
	{
		canClick = true;
	}
}
