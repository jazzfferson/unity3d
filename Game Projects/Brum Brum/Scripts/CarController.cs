using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class CarController : MonoBehaviour {

 
    private CarManager carManager;

	void Start () {

        carManager = GetComponent<CarManager>();
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ThrottlePressed();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ThrottleReleased();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BrakePressed();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            BrakeReleased();
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Steering(1);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            SteeringReleased();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Steering(-1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            SteeringReleased();
        }

    }

    public void ThrottlePressed()
    {
        carManager.ApplyThrottle(1);
    }
    public void ThrottleReleased()
    {
        carManager.ApplyThrottle(0);
    }
    public void BrakePressed()
    {
        carManager.ApplyBrake(1);
    }
    public void BrakeReleased()
    {
        print("Released");
        carManager.ApplyBrake(0);
    }
    public void Steering(int dir)
    {
        carManager.Steering(dir);
    }
    public void SliderSteering(Slider slider)
    {
        carManager.Steering(slider.value);
    }
    public void SteeringReleased()
    {
        carManager.Steering(0);
    }
}
