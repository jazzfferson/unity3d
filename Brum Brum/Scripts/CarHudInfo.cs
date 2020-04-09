using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarHudInfo : MonoBehaviour {

    public Text speedUI;
    public Slider rpmUI;
    public float maxRpm;

    public void UpdateHUD(float rpm, float speed)
    {
        rpmUI.value = rpm / maxRpm;
        speedUI.text = string.Format("{0} Km/h", Mathf.RoundToInt(speed));
    }
}
