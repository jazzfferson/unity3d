using System;
using UnityEngine;
[Serializable]
public class GetChildrensPositions
{
    public GameObject gameObjectCurve;
    private Transform[] arrayTransform;
    public Vector3[] GetPositions()
    {
        //O metodo tambem retorna o transform do gameObject pai
        arrayTransform = gameObjectCurve.GetComponentsInChildren<Transform>();

        // Segundo array que receberá apenas os transforms dos childrens
        Transform[] realTransformArray = new Transform[arrayTransform.Length - 1];

        //Adiciona ou novo array apenas os objetos do indice 1 em diante
        for (int i = 1; i < arrayTransform.Length; i++)
        {
            realTransformArray[i - 1] = arrayTransform[i];
        }

        //cria o array de vectors3
        Vector3[] vectors = new Vector3[realTransformArray.Length];

        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = realTransformArray[i].position;
        }

        return vectors;

    }
}
