using UnityEngine;
using System.Collections;

public class DebugViewer : MonoBehaviour {


    public static string[] Texto = new string[10];
	
	void Update()
	{
		if(Input.touchCount>4){Application.Quit();}
	}
    void OnGUI()
    {
        for (int i = 0; i < Texto.Length; i++)
        {
            GUI.Label(new Rect(0, i*20, 2000, 100), DebugViewer.Texto[i]);
        }
        
    }

}
