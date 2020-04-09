using UnityEngine;
using System.Collections;

public class PecaAsteroid : MonoBehaviour {
	
	
	 int life = 8;
	

	
	void InstanciarDiamante()
	{
		GameObject diamante = PoolManager.Spawn("DiamanteRoxo");
		diamante.transform.position = transform.position;
		diamante.GetComponent<Follow>().Initialize();
	
	}
	
	public void Hited(Projetil proj)
	{
		 life-=proj.powerOfDestruction;
		 if (life <= 0)
          {		
			 InstanciarDiamante();
			 PoolManager.Despawn(gameObject);
           }
		else
		{
			GetComponent<Rigidbody>().AddForceAtPosition((transform.position-proj.gameObject.transform.position).normalized * 50,proj.gameObject.transform.position);
		}
		
		
	}
	
}
