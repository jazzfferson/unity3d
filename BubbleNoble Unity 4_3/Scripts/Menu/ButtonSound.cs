using UnityEngine;
using System.Collections;
// design patterns Gang of for
public class ButtonSound : MonoBehaviour {
	
	public UISprite[] sprites;

	void Start()
	{
	  
     
        if (!PlayerPrefs.HasKey("Mute"))
        {
            PlayerPrefs.SetInt("Mute", 0);
        }

        if (PlayerPrefs.GetInt("Mute") == 1)
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

        if (Proprietes.mute)
        {
            Mute(false);
            SetTexture(false);
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            Mute(true);
            SetTexture(true);
            PlayerPrefs.SetInt("Mute", 1);
        }
       
        
	}
    void Mute(bool mute)
    {
        Proprietes.mute = mute;
    }
    void SetTexture(bool active)
    {

        if (active)
        {
            sprites[0].alpha = 0;
            sprites[1].alpha = 1;
        }
        else
        {
            sprites[0].alpha = 1;
            sprites[1].alpha = 0;
        }

    }
}
