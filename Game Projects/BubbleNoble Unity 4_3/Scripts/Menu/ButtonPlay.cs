using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {

        yield return new WaitForSeconds(1);
        Animacao();
        StartCoroutine(rotina());

	
	}

    void Animacao()
    {
        GetComponent<Animation>().Play();
    }
    IEnumerator rotina()
    {
        yield return new WaitForSeconds(Random.Range(5, 7));
        Animacao();
        StartCoroutine(rotina());
    }

}
