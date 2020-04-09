using UnityEngine;
using System.Collections;

public class FapMeme : MonoBehaviour {

    public float rotacaoMin,rotacaoMax,intervaloMin,intervaloMax,damp;

    float rotacao;
	void Start () {
        StartCoroutine(Loop());
	}

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(Random.Range(intervaloMin,intervaloMax));
        rotacao = Random.Range(rotacaoMin,rotacaoMax);
        StartCoroutine(Loop());
    }

	void Update () {


        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, rotacao, Time.deltaTime * damp));
	}
}
