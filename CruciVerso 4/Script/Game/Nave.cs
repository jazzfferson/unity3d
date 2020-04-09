using UnityEngine;
using System.Collections;
public class Jogador
{
    public int distanciaMaxMovimento;
    public int distanciaMaxAtaque;
    public string nome;

    public Jogador()
    { 
    }

}
public class Nave : MonoBehaviour {
	
	public GameObject explosao;
    public GameObject Fxs;
    public Jogador jogadorNave;

    //996463453 tel Iuri.

	[HideInInspector]public Vector3 _idTileAtual;
	
	// Use this for initialization
	void Start () {
	
		Init();
        Effects(false);
			
	}
	
	public void MoverMesmoAndar(Vector3 posicaoDestino, Vector3 idTileAtual)
	{

        _idTileAtual = idTileAtual;
		iTween.LookTo(gameObject,posicaoDestino,2f);
				
		iTween.MoveTo(gameObject,iTween.Hash("x",posicaoDestino.x,"y",posicaoDestino.y+2,"z",posicaoDestino.z,
			"speed",10f,"delay",2.1f,"easetype",iTween.EaseType.easeInOutCubic,"oncomplete","TerminouTween1"));
		
		
	}
	
	
	void TerminouTween1()
	{
		OnTerminouMover();
		
		iTween.RotateTo(gameObject,iTween.Hash("x",0,"z",0,
			"time",2f,"easetype",iTween.EaseType.easeInOutCubic,"oncomplete","TerminouTween2"));
	}
	void TerminouTween2()
	{
	}
	
	public delegate void EventHandler(GameObject NaveObject);
	public event EventHandler TerminouMover;
	
	void OnTerminouMover() 
	{
        if (TerminouMover != null)
            TerminouMover(this.gameObject);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Tiro")
		{
			Instantiate(explosao,gameObject.transform.position,Quaternion.identity);
			Destroy(gameObject);
			Debug.Log("Explode");
		}
	}
	
	void Init()
	{
		iTween.MoveAdd(gameObject,iTween.Hash("y",0.5f,
			"speed",0.1f,"easetype",iTween.EaseType.easeInOutCubic,"looptype",iTween.LoopType.pingPong));


        jogadorNave = new Jogador();
	}

    public void Effects(bool ativo)
    {
       // Fxs.SetActive(ativo);
    }
}
