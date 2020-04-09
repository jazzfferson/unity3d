using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Kmh : MonoBehaviour {

    public Image valueSprite;
    public Text valueLabel;
    public float divisor = 600f;
    float KmhValue;
    public Drivetrain driveTrain;
    


	void Start () {
	
        
	}
	
	// Update is called once per frame
	void Update () {

        valueLabel.text = ((int)driveTrain.velo).ToString();
        valueSprite.fillAmount = driveTrain.velo / divisor;

	}
}
