using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class CarController : MonoBehaviour {

    [HideInInspector]
    public PossesController possesController;
    private CarManager carManager;

	void Awake () {

        carManager = GetComponent<CarManager>();

        possesController.throttleDelegate = OnThrottle;
        possesController.brakeDelegate = OnBrake;
        possesController.steeringDelegate = OnSteering;
        possesController.gearUpDelegate = GearUp;
        possesController.gearDownDelegate = GearDown;
    }

    private void OnThrottle(float value)
    {
        if(value>0)
        {
            ThrottlePressed();
        }
        else
        {
            ThrottleReleased();
        }
    }

    private void OnBrake(float value)
    {
        if (value > 0)
        {
            BrakePressed();
        }
        else
        {
            BrakeReleased();
        }
    }

    private void OnSteering(float value)
    {
        if (value == 0)
        {
            SteeringReleased();
        }
        else
        {
            Steering(value);
        }
    }

    private void ThrottlePressed()
    {
        if (carManager.isReverse)
        {
            carManager.ApplyBrake(1);
            carManager.ApplyThrottle(0);
            carManager.isBraking = true;
        }
        else
        {
            carManager.isAcceleratig = true;
            carManager.ApplyThrottle(1);
            carManager.ApplyBrake(0);
        }
    }
    private void ThrottleReleased()
    {
        if (carManager.isReverse)
        {
            carManager.isBraking = false;
            carManager.ApplyBrake(0);
            carManager.ApplyThrottle(0);
        }
        else
        {
           carManager.isAcceleratig = false;
            carManager.ApplyThrottle(0);
            carManager.ApplyBrake(0);
        }
    }
    private void BrakePressed()
    {
        if (carManager.isReverse)
        {
            carManager.isAcceleratig = true;
            carManager.ApplyThrottle(1);
            carManager.ApplyBrake(0);
        }
        else
        {
            carManager.isBraking = true;
            carManager.ApplyThrottle(0);
            carManager.ApplyBrake(1);
        }
    }
    private void BrakeReleased()
    {
        if (carManager.isReverse)
        {
            carManager.isAcceleratig = false;
            carManager.ApplyThrottle(0);
            carManager.ApplyBrake(0);
        }
        else
        {
            carManager.isBraking = false;
            carManager.ApplyBrake(0);
            carManager.ApplyThrottle(0);
        }
    }
    private void Steering(float dir)
    {
        carManager.Steering(dir);
    }
    private void SteeringReleased()
    {
        carManager.Steering(0);
    }
    private void GearUp()
    {
        if(!carManager.automaticTransmition)
        carManager.ChangeGearUp();
    }
    private void GearDown()
    {
        if (!carManager.automaticTransmition)
            carManager.ChangeGearDown();
    }
}
