using UnityEngine;
using System.Collections;

public class AtiradorLaser : MonoBehaviour {

	// Use this for initialization
	
	
	public GameObject laser;
	public float velocidadeTiro;
	public int quantidadeDisparoBurst;
	public float intervaloDisparoBurst;
	public float precisao;
	public Light luzAtirador;
	
	
	bool DispararBurst;
	int quantDisparoRef;
	float intervaloRef;
	float intervaloDisp;
	
	Hashtable ht;
	
	void Start () {
	intervaloRef = intervaloDisparoBurst;
	quantDisparoRef = quantidadeDisparoBurst;
		
	Hashtable htf = iTween.Hash("looptype",iTween.LoopType.none,"from", 3, "to", 0f, "time", intervaloDisparoBurst, "onupdate", "updateFromValue", "easetype",iTween.EaseType.easeInOutCubic);
	ht = htf;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		DisparoBurst(velocidadeTiro,precisao);
	}
	
	//Para teste
	void DisparoUnico(float _velocidadeTiro,float _precisao)
	{
		GameObject tiro;
		
		tiro = (GameObject)Instantiate(laser,gameObject.transform.position,gameObject.transform.rotation);
		
		tiro.GetComponent<Laser>().velocidade = _velocidadeTiro;
		tiro.GetComponent<Laser>().precisao = _precisao;
		
		tiro.transform.parent = gameObject.transform;
		
		iTween.ValueTo(gameObject, ht);
		
		
	}
	
	void DisparoBurst(float _velocidadeTiro,float _precisao )
	{
		if(DispararBurst)
		{
			
			intervaloDisp-=Time.deltaTime;
		
			if(intervaloDisp<=0)
			{
				iTween.ValueTo(gameObject, ht);
				quantDisparoRef--;
				DisparoUnico(_velocidadeTiro,_precisao);
				intervaloDisp = intervaloRef;
				
				if(quantDisparoRef<=0)
				{
					quantDisparoRef = quantidadeDisparoBurst;
					DispararBurst = false;
				}
			}
		}
	}
	
	void updateFromValue(float newValue)
	{
		luzAtirador.intensity = newValue;
	}
	
	public void Atirar()
	{
		DispararBurst =true;
	}
	
}
