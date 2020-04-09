using UnityEngine;
using System.Collections;

public class ButtonOptions : MonoBehaviour {
	
	public static ButtonOptions instance;
	public Transform[] buttons;
	public UIPanel panelButtons;
	public Transform pivo;
	public GoEaseType ease;
	bool opened;
	bool enable = true;
	
	void Start()
	{
		if(instance==null)
		{
			instance = this;
		}
	}
	void Pressed()
	{

		if(!enable)
			return;
		
		enable = false;
		
		Invoke("ReEnable",1f);
		
		if(!Proprietes.canClick)
			return;
		
		Instanciador.instancia.PlaySfx(5,0.4f,1);
	

		if(!opened)
		{	
			Open();
		}
		else
		{	
			Close();	
		}
		
		
	}
	void ReEnable()
	{
		enable = true;
	}
	public void Close()
	{
			opened = false;
			for(int i = 0; i < buttons.Length; i++)
			{
				Go.killAllTweensWithTarget(buttons[i].gameObject);
				Go.to(buttons[i],Proprietes.menuButtonTimeScale, new GoTweenConfig().scale(0,false).setEaseType(ease));
				
			}
			Instanciador.instancia.PlaySfx(3,0.8f,2f);
			Go.killAllTweensWithTarget(panelButtons.gameObject);
			Go.killAllTweensWithTarget(pivo.gameObject);
			Go.to(panelButtons,Proprietes.menuButtonTimeScale/2, new GoTweenConfig().floatProp("alpha",0).setEaseType(ease));
			Go.to(pivo,Proprietes.menuButtonTimeScale, new GoTweenConfig().localRotation(new Vector3(0,0,0),false).setEaseType(ease));
	}
	void Open()
	{
			opened = true;
			for(int i = 0; i < buttons.Length; i++)
			{
				Go.killAllTweensWithTarget(buttons[i].gameObject);
				Go.to(buttons[i],Proprietes.menuButtonTimeScale, new GoTweenConfig().scale(1,false).setEaseType(GoEaseType.ElasticOut).setDelay(i*Proprietes.menuButtonTimeInterval).onStart(start=>{Instanciador.instancia.PlaySfx(3,0.8f,1.7f);}));
				
			}
			Go.killAllTweensWithTarget(panelButtons.gameObject);
			Go.killAllTweensWithTarget(pivo.gameObject);
			
			Go.to(panelButtons,Proprietes.menuButtonTimeScale, new GoTweenConfig().floatProp("alpha",1).setEaseType(ease));
			Go.to(pivo,Proprietes.menuButtonTimeScale, new GoTweenConfig().localRotation(new Vector3(0,0,200),false).setEaseType(ease));
	}
	
	
	
}
