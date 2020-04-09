using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarManager : MonoBehaviour
{
    public AnimationCurve steeringCurve;
    public float frontTorqueDistribuitionSteering, backTorqueDistribuitionSteering;
    public CarHudInfo carHudUI;
    public float smoothnessThrottle, smoothnessSteering;
    public float gearUpRpm, gearDownRpm;

    private Rigidbody rigidBody;
    private Motor motor;
    private Transmsition transmissao;
    private Steering steering;
    private Brake brake;
    private SoundEngine soundEngine;
    private float wheelRadius;
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


   
    void FixedUpdate()
    {

        float finalThrottle=0;
        if (transmissao.IsChangingGear)
        {
            finalThrottle = 0;
        }
        else
        {
            if (throttleAmount != 0)
            finalThrottle = Mathf.Lerp(motor.Throttle, throttleAmount, Time.fixedDeltaTime * smoothnessThrottle);
        }

        motor.MotorUpdate(transmissao.ActualGearRatio, transmissao.finalDriveRatio, finalThrottle);
        brake.ApplyBrake(brakeAmount);

        
    }
    void Update()
    {
        AutomaticTransmition();
        steering.SteeringAmount(steeringCurve.Evaluate(steringAmount));
        motor.BackTorqueDistribuition = (0.5f + (-(steering.Amount * backTorqueDistribuitionSteering )/ steering.maxAngle) / 2);

        carHudUI.UpdateHUD(soundEngine.finalRpm, rigidBody.velocity.magnitude * 3.6f);
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
    float speedKmh;
    void AutomaticTransmition()
    {
        float wheelMaxRpm = ((gearUpRpm - motor.minMotorRpm) / transmissao.ActualGearRatio / transmissao.FinalDrive);
        float speedToChangeGearUp = MathHelper.WheelRpmToKph(wheelMaxRpm, wheelRadius);

        float wheelMinRpm = ((gearDownRpm - motor.minMotorRpm) / transmissao.ActualGearRatio / transmissao.FinalDrive);
        float speedToChangeGearDown = MathHelper.WheelRpmToKph(wheelMinRpm, wheelRadius);


        speedKmh = MathHelper.ConvertMeterPerSecondToKph(rigidBody.velocity.magnitude);
      
        if (speedKmh >= speedToChangeGearUp && !transmissao.IsChangingGear && transmissao.GearIndex < transmissao.gearsRatio.Length - 1)
        {
            transmissao.ChangeGear(transmissao.GearIndex + 1);
        }
        else if (speedKmh <= speedToChangeGearDown && !transmissao.IsChangingGear && transmissao.GearIndex > 0)
        {
            transmissao.ChangeGear(transmissao.GearIndex - 1);
        }
    }


}

