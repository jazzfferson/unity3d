using UnityEngine;
using System.Collections;

public class Angle : MonoBehaviour {

	
	public int sensibilidade;
	Vector3 diferenca;
	bool podeRodar = false;
	void Start()
	{
		diferenca = new Vector3(0,0,sensibilidade/2);
		Invoke("PodeRodar",0.2f);
	}
	void OnSliderChange(float valor) {
		
		if(podeRodar)
		{
			transform.eulerAngles =  Vector3.forward * valor * sensibilidade - diferenca;
		}
	}
	void PodeRodar()
	{
		podeRodar = true;
		OnSliderChange(0.5f);
	}
	
}
