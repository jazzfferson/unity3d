using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public GameObject particulas;
    public float velocidade;

	void Start () {
		
		Destroy(gameObject,1);
	
	}
	
	
	void Update () {

        transform.Translate(Vector3.forward * Time.deltaTime * velocidade);
	
	}
    

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "ColisorPersonagem")
        {
            GameData.numVidas-=0.5f;
            //Handheld.Vibrate();
        }
		
        GameObject parti = (GameObject)Instantiate(particulas, this.transform.position, Quaternion.identity);
        Destroy(parti, 0.8f);
        Destroy(this.gameObject);
    }
}
