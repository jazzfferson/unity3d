using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour {

    public static Scores instance;
    public UILabel scoreLabel, highScoreLabel;

	void Awake () {

        if (instance == null) { instance = this; }
	
	}
	
	public void SetScore(int valor)
    {
        scoreLabel.text = valor.ToString();
    }

    public void SetHighScore(int valor)
    {
        highScoreLabel.text = valor.ToString();
    }
}
