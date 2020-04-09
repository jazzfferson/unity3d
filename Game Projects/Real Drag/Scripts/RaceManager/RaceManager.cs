using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {

	public CarManager Carro;
	public UILabel labelContador;
	public UILabel labelContador2;
	int contador = 3;
	bool timer;
	float tempoCorrida;
	
	IEnumerator Start () {
		
		
		yield return new WaitForSeconds(0.1f);
		labelContador.text = "Tempo Corrida: " +tempoCorrida.ToString();
		Carro.transmition.ChangeGear(1);
		Carro.brake.ApplyBrake(1);
		StartCoroutine("Contador");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Timer(timer);
		if(Carro.transform.position.z>=402)
		{
			timer = false;
		}
		
	
	}
	void StartCorrida()
	{
		Carro.Engatar();
		Carro.brake.ApplyBrake(0);
		timer = true;
	}
	IEnumerator Contador()
	{
		yield return new WaitForSeconds(1);
		if(contador>1)
		{
			contador--;
			labelContador2.text = contador.ToString();
			StartCoroutine("Contador");
		}
		else
		{
			contador--;
			labelContador2.text = contador.ToString();
			StartCorrida();
		}
		
	}
	
	void Timer(bool contar)
	{
		if(contar)
		{
			tempoCorrida+=Time.deltaTime;
			labelContador.text = "Tempo Corrida: " +tempoCorrida.ToString();
		}
		else
		{
			tempoCorrida = 0;
		}
	}
}
