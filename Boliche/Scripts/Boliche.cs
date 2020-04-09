using UnityEngine;
using System.Collections;

public class Boliche : MonoBehaviour {
	
	
	public GameObject Lancador;
	public GameObject Resetor;
		
	public int ForcaLancamento;

	public GameObject cameraJogo;
	
	

	
	// Use this for initialization
	void Start () {
		
		Eventos();
	}
	
	void Eventos()
	{
		Lancador.GetComponent<Lancador>().BolaRef.GetComponent<Bola>().JogadaInvalida+=JogadaInvalida;
	}

	void JogadaInvalida (GameObject e)
	{
		   StartCoroutine(ReacaoJogadaInvalida());
	}
	
	IEnumerator ReacaoJogadaInvalida()
	{	
		yield return new WaitForSeconds(0.2f);
		cameraJogo.GetComponent<CameraJazz>().ativa=false;
		yield return new WaitForSeconds(1f);
		Lancador.GetComponent<Lancador>().ResetarBolaCamera();
		Eventos();	
	}
	IEnumerator ReacaoJogadaValida()
	{	
		yield return new WaitForSeconds(7f);
		Lancador.GetComponent<Lancador>().ResetarBolaCamera();
		Eventos();	
	}
	
	// Update is called once per frame
	void Update () {


	 Controle();
		
	
	}

	void Controle()
	{
		//Move o lancador
		if(Input.GetKey(KeyCode.D))
		Lancador.GetComponent<Lancador>().Move(new Vector3(1,0,0));
		
		else if(Input.GetKey(KeyCode.A))
		Lancador.GetComponent<Lancador>().Move(new Vector3(-1,0,0));
		
			
		
		//Rotaciona o lancador
		if(Input.GetKey(KeyCode.UpArrow))
		Lancador.GetComponent<Lancador>().Rotaciona(new Vector2(0,-1));
		
		else if(Input.GetKey(KeyCode.DownArrow))
		Lancador.GetComponent<Lancador>().Rotaciona(new Vector2(0,1));
		
		if(Input.GetKey(KeyCode.RightArrow))
		Lancador.GetComponent<Lancador>().Rotaciona(new Vector2(1,0));
		
		else if(Input.GetKey(KeyCode.LeftArrow))
		Lancador.GetComponent<Lancador>().Rotaciona(new Vector2(-1,0));
		
		//Lança a bola
		if(Input.GetKeyDown(KeyCode.Space))
			Lancador.GetComponent<Lancador>().Lancar(ForcaLancamento);
		
		//Reseta a bola (TESTE)
		if(Input.GetKeyDown(KeyCode.R))
			Lancador.GetComponent<Lancador>().ResetarBolaCamera();
		
		//Resetar pinos
		//if(Input.GetKeyDown(KeyCode.P))
			//Resetor.GetComponent<Resetor>().Limpar();
			
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Bola")
		{
			
			Resetor.GetComponent<Resetor>().Barreira();
			StartCoroutine(ReacaoJogadaValida());
		}
	}
}
