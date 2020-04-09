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
    public static bool mute = false; 
    public static EstadoMenu estadoMenu = EstadoMenu.Inicial;
    public static EstadoJogo estadoJogo = EstadoJogo.Jogando;
    
}
