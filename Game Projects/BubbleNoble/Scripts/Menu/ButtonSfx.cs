using UnityEngine;
using System.Collections;
// design patterns Gang of for
public class ButtonSfx : MonoBehaviour {
	
	public UISprite icon;

	void Start()
	{  
     
        if (!PlayerPrefs.HasKey("MuteSfx"))
        {
            PlayerPrefs.SetInt("MuteSfx", 0);
        }

        if (PlayerPrefs.GetInt("MuteSfx") == 1)
        {
            Mute(true);
            SetTexture(true);
        }
	}
	void Pressed()
	{
		if(!Proprietes.canClick)
			return;
		
		Instanciador.instancia.PlaySfx(3,0.4f,1);

        if (Proprietes.muteSfx)
        {
            Mute(false);
            SetTexture(false);
            PlayerPrefs.SetInt("MuteSfx", 0);
        }
        else
        {
            Mute(true);
            SetTexture(true);
            PlayerPrefs.SetInt("MuteSfx", 1);
        }
       
        
	}
    void Mute(bool mute)
    {
        Proprietes.muteSfx = mute;
    }
    void SetTexture(bool active)
    {

        if (active)
        {
            icon.spriteName = "icon_sound_off";
        }
        else
        {
            icon.spriteName = "icon_sound_on";
        }

    }
}
