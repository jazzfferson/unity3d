using UnityEngine;
using System.Collections;
using System;

public class StaticReferences : MonoBehaviour {

    public bool ready = false;

    public static StaticReferences Instance;
   
    public GameObject car;

    public Camera MainCamera;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

}
