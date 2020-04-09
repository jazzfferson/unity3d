using UnityEngine;
using System.Collections;
public enum EstadoMenu{Inicial,Escolhefase,Info,Tutorial};
public enum EstadoJogo{Jogando,Pausado,WinScreen};


public class Proprietes  {

    public static int casasAtivas;
    public static bool canClick = true;
    public static float menuButtonTimeScale = 0.8f;
    public static float menuButtonTimeInterval = 0.2f;
    public static int jogadasEfetuadas;
    public static bool muteSfx = false;
    public static bool muteMusic = false;
    public static float musicTime = 0;
    public static EstadoMenu estadoMenu = EstadoMenu.Inicial;
    public static EstadoJogo estadoJogo = EstadoJogo.Jogando;

    public static void MusicFadeOut(AudioSource audioSource)
    {
        Go.to(audioSource, 0.6f, new GoTweenConfig().floatProp("volume", 0, false).
            onComplete(Completed => { Proprietes.musicTime = audioSource.time; }));
    }
    public static void MusicFadeIn(AudioSource audioSource)
    {
        audioSource.time = Proprietes.musicTime;
        Go.to(audioSource, 1.6f, new GoTweenConfig().floatProp("volume", 1, false));
    }
    
}
