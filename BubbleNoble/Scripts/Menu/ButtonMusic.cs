using UnityEngine;
using System.Collections;

public class ButtonMusic : MonoBehaviour {

    public UISprite icon;
    public AudioSource audio;

	void Start () {

        if (!PlayerPrefs.HasKey("MuteMusic"))
        {
            PlayerPrefs.SetInt("MuteMusic", 0);
        }

        if (PlayerPrefs.GetInt("MuteMusic") == 1)
        {
            Mute(true);
            SetTexture(true);
        }
	}
	
	

    void Mute(bool mute)
    {
        Proprietes.muteMusic = mute;
        audio.mute = mute;
    }
    void SetTexture(bool active)
    {

        if (active)
        {
            icon.spriteName = "icon_music_off";
        }
        else
        {
            icon.spriteName = "icon_music_on";
        }

    }
    void Pressed()
    {
        if (!Proprietes.canClick)
            return;

    
        if (Proprietes.muteMusic)
        {
            Mute(false);
            SetTexture(false);
            PlayerPrefs.SetInt("MuteMusic", 0);
        }
        else
        {
            Mute(true);
            SetTexture(true);
            PlayerPrefs.SetInt("MuteMusic", 1);
        }
    }
}
