using UnityEngine;
using System.Collections;

public class StatisticsEndLess : MonoBehaviour
{

    public static StatisticsEndLess instance;

    public UILabel time, highScore, score, maxCombo, words, corrects, incorrects;
    public int timeValue, highScoreValue, scoreValue, maxComboValue, wordsValue, correctsValue, incorrectsValue;
 
	void Start () {

        if (instance == null) {instance = this;}
	
	}

    public void ShowResults()
    {

        int totalSeconds = timeValue;
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        string secsTrick = "";
        string minTrick = "";

        if (seconds < 10)
        {
            secsTrick = "0";
        }
        if (minutes < 10)
        {
            minTrick = "0";
        }

        string timeFormated = minTrick+minutes + ":" + secsTrick + seconds;
        time.text = timeFormated;

        highScore.text = highScoreValue.ToString();
        score.text = scoreValue.ToString();
        maxCombo.text = maxComboValue.ToString();
        words.text = wordsValue.ToString();
        corrects.text = correctsValue.ToString();
        incorrects.text = incorrectsValue.ToString();
    }
}
