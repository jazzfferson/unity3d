using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	public int powerOfDestruction;
    float vel;
	Projetil projetil;
	
    public void Init(float precisao, float velocidade)
    {
        transform.Rotate( 0,Random.Range(-precisao, precisao), 0);
        vel = velocidade;
		projetil.powerOfDestruction = powerOfDestruction;
		projetil.gameObject = gameObject;
    }
	void Update () {
		
		transform.Translate(Vector3.forward * vel * Time.deltaTime,Space.Self);
	}
	void OnTriggerExit(Collider other)
	{
		if(other.name=="BoundsWorld")
		{
			PoolManager.Despawn(gameObject);
		}		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Asteroid")
		{
			Instanciador.instancia.PlaySfx(2,1);
			Destroy(Instanciador.instancia.Instanciar(0,transform.position,transform.rotation),5);
			PoolManager.Despawn(gameObject);
			
			
			other.SendMessage("Hited",projetil,SendMessageOptions.DontRequireReceiver);
		}
	}
	
}

public struct Projetil
{
	public GameObject gameObject;
	public int powerOfDestruction;
}