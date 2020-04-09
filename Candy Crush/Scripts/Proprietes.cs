using UnityEngine;
using System.Collections;

public enum Language:int {English = 0,Portugues=1,Espanhol=2,Nihongo=3,Francais=4};
public enum Mode:int {Pratice=0,Endless=1};

public class Proprietes {

    public static Language Language
    {
        get;
        set;
    }

    public static Mode Mode
    {
        get;
        set;
    }

    public static bool MuteAudio
    {
        get;
        set;
    }

    public static float MusicVolume
    {
        get;
        set;
    }

    public static int HighScore
    {
      
		#region Get
		get
		{
            if (PlayerPrefs.HasKey("HighScore"))
			{
                return PlayerPrefs.GetInt("HighScore");
			}
		   else
		    {
                PlayerPrefs.SetInt("HighScore", 0);
                return PlayerPrefs.GetInt("HighScore");
		    }
		}
		#endregion
		
		#region Set
		
		set
		{

            PlayerPrefs.SetInt("HighScore", value);
			PlayerPrefs.Save();
		}
		#endregion
	
    }

    public static int Score
    {
        get;
        set;
    }

   

    

	
}
