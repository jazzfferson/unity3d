using UnityEngine;
using System.Collections;

public class Debugger : MonoBehaviour {

    public static void DebugLog(object obj,bool error)
    {
        if (error)
            Debug.LogError(obj);
        else
        Debug.Log(obj);
    }
}
