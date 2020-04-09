using UnityEngine;
using System.Collections;
using System;

public static class Wp8Unity
{
    public delegate void Wp8Delegate();
    public static event Wp8Delegate OnCurtir, OnCompartilhar,OnInterstitial;
    public static void Curtir()
    {
        if (OnCurtir != null)
        {
            OnCurtir();
        }
    }
    public static void Compartilhar()
    {
        if (OnCompartilhar != null)
        {
            OnCompartilhar();
        }
    }
    public static void Interstitial()
    {
        if (OnInterstitial != null)
        {
            OnInterstitial();
        }
    }

}
