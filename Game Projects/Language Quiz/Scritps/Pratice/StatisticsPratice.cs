using UnityEngine;
using System.Collections;

public class StatisticsPratice : MonoBehaviour {

    public static StatisticsPratice instance;
    public UILabel quant;
    public UILabel percent;

	void Start () {

        if (instance == null) {instance = this;}
	
	}

    public void ShowResults(int rightQuestions)
    {
        float rtq = rightQuestions;
        float prcnt = 100f / 6f;
        float prcntTotal = prcnt * rtq;

        if ((int)prcntTotal != prcntTotal)
        {
            percent.text = "(" + string.Format("{0:0.0}", prcntTotal) + "%)";
        }
        else
        {
            percent.text = "(" + prcntTotal.ToString() + "%)";
        }
        quant.text = rightQuestions.ToString() + "/6";

       

    }
}
