using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PinheirosPosicionamento : MonoBehaviour {

    public GameObject prefab;
    List<GameObject> prefabArray;
    public Transform inicio,fim;
    public float random;
    public float intervalo;
    public int quantidade;
    float Posicao;
    Vector3 rayPosition;
    public bool atualizar;
    
    

	void Start () {

      
	}
    void Update()
    {
        if (!atualizar)
            return;
        atualizar = false;

        if (prefabArray != null)
        {
            foreach(GameObject obj in prefabArray)
            {
                DestroyImmediate(obj);
            }

            prefabArray.Clear();
        }
        prefabArray = new List<GameObject>();
        RaycastHit hit;
        rayPosition = inicio.position;

        for (int i = 0; i < quantidade; i++)
        {

            if (Physics.Raycast(rayPosition, -transform.up, out hit))
            {
                if (hit.collider.CompareTag("Grama"))
                {
                    prefabArray.Add((GameObject)Instantiate(prefab, hit.point, Quaternion.Euler(new Vector3(0,Random.Range(0,360),0))));
                }
            }

            if (rayPosition.z >= fim.position.z)
            {
                rayPosition = new Vector3(rayPosition.x += intervalo, 100, inicio.position.z);
                rayPosition += new Vector3(Random.RandomRange(-random, random), 0, Random.RandomRange(-random, random));
            }
            else
            {
                rayPosition += new Vector3(0, 0, intervalo);
                rayPosition += new Vector3(Random.RandomRange(-random, random), 0, Random.RandomRange(-random, random));
            }
        }          
    }
}
