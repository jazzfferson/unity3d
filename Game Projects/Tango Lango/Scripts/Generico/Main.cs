using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public static Main instance;

	void Awake () {
		
		 DontDestroyOnLoad(this.gameObject);
    
	}
	void Start()
	{
		if(instance==null)
		{
			instance= this;
			return;
		}
		Destroy(this.gameObject);
	}
}
