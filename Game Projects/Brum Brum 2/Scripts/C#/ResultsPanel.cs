using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsPanel : MonoBehaviour {

    [SerializeField]
    private Image fadeBG;

    [SerializeField]
    private Text targetTimeUI, myTimeUI, differenceTimeUI;

    [SerializeField]
    private RectTransform panelTransform;

    [SerializeField]
    private float alphaSpeed , panelSpeed;

    //[SerializeField]
    //private GoEaseType panelEaseType;


    void Awake()
    {
        fadeBG.color = new Color(0, 0, 0, 0);
        gameObject.SetActive(false);
    }

    public void ShowResults(float targetTime,float myTime)
    {

       gameObject.SetActive(true);

        targetTimeUI.text = StringHelper.FloatToTime(targetTime, "#0'00:000");
        myTimeUI.text = StringHelper.FloatToTime(myTime, "#0'00:000");

        float difference = targetTime - myTime;
        string positiveChar = "";
        Color color;

        if(difference<0)
        {
            color = Color.red;
            positiveChar = "- ";
        }
        else
        {
            color = Color.green;
            positiveChar = "+ ";
        }
        differenceTimeUI.color = color;
        differenceTimeUI.text = positiveChar + StringHelper.FloatToTime(Mathf.Abs(difference), "#0'00:000");
        //Go.to(panelTransform, panelSpeed, new GoTweenConfig().vector2Prop("anchoredPosition",Vector2.zero, false).setEaseType(panelEaseType).setDelay(2));
        //Go.to(fadeBG, alphaSpeed, new GoTweenConfig().colorProp("color", new Color(0, 0, 0, 0.7f), false).setEaseType(panelEaseType).setDelay(2));
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
    public void MainMenu()
    {

    }
}
