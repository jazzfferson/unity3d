using UnityEngine;
using System.Collections;
using System;


public class PlaceObject : MonoBehaviour
{
    public int applyOnLayerMask;
    public GameObject[] listaPrefabs;
    public Transform parent;
    public int instanciarPrefab;
    public bool aleatorio;
    public bool rotacaoYAleatoria;

    public GameObject GetRandom()
    {
        return listaPrefabs[UnityEngine.Random.Range(0,listaPrefabs.Length)];
    }
    public GameObject Get(int index)
    {
        return listaPrefabs[index];
    }
    public Quaternion RandomRotation()
    {
        return Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0);
    }
}