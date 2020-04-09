using UnityEngine;
using System.Collections;

public class MainCode : MonoBehaviour {
	
	
	public UILabel distance;
	
	public TweenScale landed;
	public TweenScale failed;
	
	public Foguete foguete;
	Vector3 posicaoFoguete;
	Quaternion rotationFoguete;
	
	bool haslauched;
	bool haslanded;
	public Transform planeta;
    float forca;
    float tempoPropulsao;
	
	void Start () {	
		
		foguete.OnCaiu+= HandleFogueteOnCaiu;
		foguete.OnMorreu+= HandleFogueteOnMorreu;	
		
	}

	void HandleFogueteOnCaiu (object sender)
	{
		
		
		if(haslauched && foguete.hasMoon)
		{
			landed.gameObject.SetActive(true);
			haslanded = true;
		}
		else
		{
			
		}
	}
	void HandleFogueteOnMorreu(object sender)
	{
		failed.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
		distance.text = string.Format("{0:0.00}", Vector3.Distance(planeta.position,foguete.gameObject.transform.position)-5.48f) + " KM";
		
	}
	void Lauch()
	{
		foguete.Lauch(forca,tempoPropulsao);
		haslauched = true;
		StartCoroutine("lauchedFoguete");
	}
	void Reset()
	{
		foguete.Reset();
		haslanded = false;
		landed.gameObject.SetActive(false);
		failed.gameObject.SetActive(false);
	}
	
	void OnSliderChange(float valor)
	{
        forca = valor * 100;
	}
	void OnSliderChange2(float valor)
	{
		tempoPropulsao = valor*5;
	}
	
	IEnumerator lauchedFoguete()
	{
		yield return new WaitForSeconds(20);
		if(!haslauched)
		failed.gameObject.SetActive(true);
	}
}
