using UnityEngine;
using System.Collections;

public class Metralhadora : MonoBehaviour {

	
	public Transform[] spawPositions;
	public float precisao;
	public float velocidade;
	public float intervaloTiro;
	bool tiroGerado = false;
	public Material materialTiro;
	bool continuaAtirando;
	void Start () {
		
		LevelMetralhadora(3);
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey("space"))
			StartCoroutine(GerarTiro());
		
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			LevelMetralhadora(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			LevelMetralhadora(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			LevelMetralhadora(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			LevelMetralhadora(3);
		}
		
	}
	
	public void Gerar()
	{
		if(!tiroGerado)
		StartCoroutine(GerarTiroTouch());
		continuaAtirando = true;
	}
	public void PararGerar()
	{
		continuaAtirando = false;
	}
	
	 IEnumerator GerarTiro()
	{
		
		if(!tiroGerado)
		{
		AudioManager.instance.PlayLaser();
		
		tiroGerado = true;
			
		RequestTiro();
		
		yield return new WaitForSeconds(intervaloTiro);
		tiroGerado = false;
		}
		
	}
	 IEnumerator GerarTiroTouch()
	{
		
		Instanciador.instancia.PlaySfx(1,1);
		
		tiroGerado = true;
		
		RequestTiro();
		
		yield return new WaitForSeconds(intervaloTiro);
		tiroGerado = false;
		
		if(continuaAtirando)
	    StartCoroutine(GerarTiroTouch());
		
	}
	void RequestTiro()
	{
		GameObject tiro;
		foreach(Transform posicaoInstancia in spawPositions)
		{
			
			tiro = PoolManager.Spawn("Tiro");
			if(tiro!=null)
			{
				tiro.transform.position = posicaoInstancia.position;
				tiro.transform.rotation = transform.rotation;
				tiro.GetComponent<Tiro>().Init(precisao,velocidade + GetComponent<Rigidbody>().velocity.magnitude);
			}
		}
	}
	public void LevelMetralhadora(int level)
	{
	   
		switch(level)
		{
			
			case 0:
			precisao = 0;
			intervaloTiro = 0.5f;
			velocidade = 90;
			materialTiro.SetColor("_EmisColor", new Color(0,1,0,0.5f));
			break;
			
			case 1:
			precisao = 0;
			intervaloTiro = 0.2f;
			velocidade = 110;
			materialTiro.SetColor("_EmisColor", new Color(1,0.4f,0,0.5f));
			break;
			
			case 2:
			precisao = 0;
			intervaloTiro = 0.15f;
			velocidade = 150;
			materialTiro.SetColor("_EmisColor", new Color(1,0,0,0.5f));
			break;
			
			case 3:
			precisao = 0;
			intervaloTiro = 0.1f;
			velocidade = 190;
			materialTiro.SetColor("_EmisColor", new Color(0,0,1,0.7f));
			break;
			
		}
	}
	
	
}
