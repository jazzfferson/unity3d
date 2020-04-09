using UnityEngine;
using System.Collections;

public class ButtonInfo : MonoBehaviour {

	
	public OratorMessage orator;
	public float time;
	public GoEaseType type;
	public Transform[] pivos;
	public static bool info;
	private bool canPress = true;
	
	void Start () {

	
	}
	void Update()
	{
		
		if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote)) && info && canPress)
		MenuDown();	
	}
    void Pressed()
    {
		if(!Proprietes.canClick || !canPress)
			return;
	
	
	Proprietes.estadoMenu = EstadoMenu.Info; 	
        canPress = false;
	ButtonOptions.instance.Close();
	Instanciador.instancia.PlaySfx(3,0.4f,1);
        info = true;
	    foreach(Transform pivo in pivos)
        Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(0, -437, 0), false).setEaseType(GoEaseType.CubicInOut).onComplete(lambda =>{orator.ExibeMessage("");canPress = true;}));

    }
    void MenuDown()
    { 
	if(!Proprietes.canClick || !canPress)
			return;
	
	Proprietes.estadoMenu = EstadoMenu.Inicial; 
	canPress = false;
	orator.HideMessage();
	info = false;
	foreach(Transform pivo in pivos)	
        Go.to(pivo, 0.8f, new GoTweenConfig().localPosition(new Vector3(0, 0, 0), false).setEaseType(GoEaseType.CubicInOut).onComplete(Completed => {canPress = true;}));
		
         
    }

}
