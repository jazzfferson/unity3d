using UnityEngine;
using System.Collections;
using System;

public class Tile:MonoBehaviour
{
    void Awake()
    {
       gameObject.isStatic = true;
    }
}
