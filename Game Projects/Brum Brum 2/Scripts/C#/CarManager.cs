using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarManager : MonoBehaviour
{
    public AnimationCurve steeringCurve;
    public float torqueDistribuitionSteering;
    public CarHudInfo carHudUI;
    public float smoothnessSteering = 0.5f;
    public float smoothnessThrotlle = 0.5f;
    public float smoothnessBrake = 0.5f;
    public float gearUpRpm, gearDownRpm;
    public bool automaticTransmition = true;

    private Rigidbody rigidBody;
    private Motor motor;
    private Transmsition transmissao;
    private Steering steering;
    private Brake brake;
    private SoundEngine soundEngine;
    private float wheelRadius;
    public bool isBraking;
    public bool isAcceleratig;
    public bool isReverse;
    public Transform centerOffMass;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        motor = GetComponent<Motor>();
        transmissao = GetComponent<Transmsition>();
        steering = GetComponent<Steering>();
        steering.Smooth = smoothnessSteering;
        brake = GetComponent<Brake>();
        soundEngine = GetComponent<SoundEngine>();
        transmissao.ChangeGear(0);
        wheelRadius = steering.wheels[0].radius;
        rigidBody.centerOfMass = centerOffMass.localPosition;

    }

    float throttleVelocity;
    float brakeVelocity;

    void FixedUpdate()
    {
        SpeedKmh = MathHelper.ConvertMeterPerSecondToKph(rigidBody.velocity.magnitude);

        float finalThrottle = throttleAmount;
        float finalBrake = brakeAmount;

        if(automaticTransmition)
        AutomaticTransmition();

        if (transmissao.IsChangingGear)
        {
            finalThrottle = 0;
        }
        else
        {
            if(finalThrottle > 0)
            finalThrottle = Mathf.SmoothDamp(motor.Throttle, finalThrottle, ref throttleVelocity, smoothnessThrotlle);
        }

        if(finalBrake>0)
        finalBrake = Mathf.SmoothDamp(brake.Amount, finalBrake, ref brakeVelocity, smoothnessBrake);

        motor.MotorUpdate(transmissao.ActualGearRatio, transmissao.finalDriveRatio, finalThrottle);
        brake.ApplyBrake(finalBrake);
        motor.BackRightLeftTorqueDistribuition = (0.5f + (-(steering.Amount * torqueDistribuitionSteering) / steering.maxAngle) / 2);
        steering.SteeringAmount(steeringCurve.Evaluate(steringAmount));


        //TEMPORARIO
        if(Input.GetKey(KeyCode.Space))
        {
            brake.ApplyHandBrake();
        }

    }
    void Update()
    { 
        carHudUI.UpdateCarHUD(soundEngine.finalRpm, rigidBody.velocity.magnitude * 3.6f, transmissao.GearIndex, isReverse);
    }
    float brakeAmount;
    public void ApplyBrake(float amount)
    {
        brakeAmount = amount;
    }
    float throttleAmount;
    public void ApplyThrottle(float amount)
    {
        throttleAmount = amount;
    }
    float steringAmount;
    public void Steering(float amount)
    {
        steringAmount = amount;
    }
    public float SpeedKmh
    {
        get;
        private set;
    }
    void AutomaticTransmition()
    {
        float wheelMaxRpm = ((gearUpRpm - motor.minMotorRpm) / transmissao.ActualGearRatio / transmissao.FinalDrive);
        float speedToChangeGearUp = MathHelper.WheelRpmToKph(wheelMaxRpm, wheelRadius);

        float wheelMinRpm = ((gearDownRpm - motor.minMotorRpm) / transmissao.ActualGearRatio / transmissao.FinalDrive);
        float speedToChangeGearDown = MathHelper.WheelRpmToKph(wheelMinRpm, wheelRadius);


        if (SpeedKmh < 2 & !transmissao.IsChangingGear)
        {

            if (!isReverse)
            {
                if (isBraking)
                {
                    isBraking = false;
                    isReverse = true;
                    transmissao.ChangeGear(1);
                    motor.SetReverse(true);
                }
            }
            else
            {
                if (isBraking)
                {
                    isBraking = false;
                    isReverse = false;
                    transmissao.ChangeGear(0);
                    motor.SetReverse(false);
                }
            }


        }
        if (!isReverse)
        {
            if (SpeedKmh >= speedToChangeGearUp && !transmissao.IsChangingGear && isAcceleratig && motor.ActualRpm >= gearUpRpm)
            {
                ChangeGearUp();
            }
            else if (SpeedKmh <= speedToChangeGearDown && !transmissao.IsChangingGear && !isAcceleratig)
            {
                ChangeGearDown();
            }
        }
    }

    public void ChangeGearUp()
    {
        if(transmissao.GearIndex < transmissao.gearsRatio.Length - 1)
        transmissao.ChangeGear(transmissao.GearIndex + 1);
    }
    public void ChangeGearDown()
    {
        if (transmissao.GearIndex > 0)
        transmissao.ChangeGear(transmissao.GearIndex - 1);
    }
}

