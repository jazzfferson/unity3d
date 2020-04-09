using UnityEngine;
using System.Collections;

public class ButtonAudio : MonoBehaviour {

    public UI2DSprite icon;
    public AudioSource audio1,audio2;
    public Sprite On, Off;

    void Start()
    {

        if (!PlayerPrefs.HasKey("MuteAudio"))
        {
            PlayerPrefs.SetInt("MuteAudio", 0);
        }

        if (PlayerPrefs.GetInt("MuteAudio") == 1)
        {
            Mute(true);
            SetTexture(true);
        }
    }

    void Mute(bool mute)
    {
        Proprietes.MuteAudio = mute;
        audio1.mute = mute;
		audio2.mute = mute;
    }
    void SetTexture(bool active)
    {

        if (active)
        {
            icon.sprite2D = Off;
        }
        else
        {
            icon.sprite2D = On;
        }

    }
    void Pressed()
    {


        if (Proprietes.MuteAudio)
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
