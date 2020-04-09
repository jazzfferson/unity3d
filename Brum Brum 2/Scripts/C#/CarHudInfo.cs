using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CarHudInfo : MonoBehaviour {

    public Text totalLapsUI,playerLapsUI;
    public Text speedUI;
    public Text gearUI;
    public Image rpmUI;
    public Text targetTimeUI, actualLapTimeUI,totalTimeUI;
    public int rpmOffset;
    public float maxRpm;
    private float dif = 0.65f;

    public void SetStaticValues(float targetTime, int laps, int playerLap)
    {
        totalLapsUI.text = string.Format("/{0}", laps);
        playerLapsUI.text = playerLap.ToString();
        targetTimeUI.text = StringHelper.FloatToTime(targetTime, "#0'00:000");
    }


    public void SetPlayerTimeColor(Color color)
    {
        actualLapTimeUI.color = color;
    }

    public void UpdateCarHUD(float rpm, float speed, int gear, bool isReverse)
    {
        string gearText = string.Empty;

        if (isReverse)
        {
            gearText = "R";
        }
        else
        {
            gearText = (1+gear).ToString();
        }

        gearUI.text = gearText;

        rpmUI.fillAmount = Mathf.Lerp(0f, dif, (rpm/maxRpm) * dif);
        speedUI.text = Mathf.RoundToInt(speed).ToString();
    }

    public void UpdateGameHUD(float totalTime,float actualLapTime)
    {
        actualLapTimeUI.text = StringHelper.FloatToTime(actualLapTime, "#0'00:000");
        totalTimeUI.text = StringHelper.FloatToTime(totalTime, "#0'00:000");
    }
}
