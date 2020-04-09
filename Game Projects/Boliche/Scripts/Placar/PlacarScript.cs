using UnityEngine;
using System.Collections;

public class PlacarScript : MonoBehaviour {
	public int positionX;
	public int positionY;
	public Texture2D texture;
	public GUIStyle guiStyle;
	
	public static bool changeFrame;
	public static int frameAtual;
	public static Frame[] frames;
	
	// Use this for initialization
	void Start () {
		frames = new Frame[10];
		for(int i = 0; i < frames.Length; i++)
		{
			if(i == 9)
				frames[i] = new FrameFinal();
			else
				frames[i] = new FrameNormal();
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool toChangeFrame = frames[frameAtual].VerificarJogada();
		if(toChangeFrame)
		{
			frameAtual++;
			changeFrame = true;
		}
	}
	
	void OnGUI()
	{
		float btnSize = 550 / 12;
		for (int i = 0; i < 10; i++)
		{
			GUI.Box(new Rect(i * btnSize + positionX,positionY,btnSize,btnSize), texture);
			if(frames[i].tipoJogada == StaticMembers.TiposDeJogadas.Strike)
			{
				GUI.Box(new Rect(i * btnSize + positionX + 10,positionY,btnSize,btnSize), "X", guiStyle);
			}
			else if(frames[i].tipoJogada == StaticMembers.TiposDeJogadas.Spare)
			{
				GUI.Box(new Rect(i * btnSize + positionX + 10,positionY,btnSize,btnSize), "S", guiStyle);
			}
			else
			{
				GUI.Box(new Rect(i * btnSize + positionX,positionY,btnSize,btnSize), frames[i].pinosDerrubados[0] + "/" + frames[i].pinosDerrubados[1], guiStyle);
			}
		}
		
		
		GUI.Box(new Rect(0, Screen.height - 300, 300, 300),"FRAME ATUAL : " + frameAtual.ToString() + "  JOGADA ATUAL: " + frames[frameAtual].jogadaAtual.ToString());
	}
}
