using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarManager : MonoBehaviour
{
    public AnimationCurve steeringCurve;
    public float frontTorqueDistribuitionSteering, backTorqueDistribuitionSteering;

    public float smoothnessThrottle, smoothnessSteering;
    public float gearUpRpm, gearDownRpm;

    private Rigidbody rigidBody;
    private Motor motor;
    private Transmsition transmissao;
    private Steering steering;
    private Brake brake;
    private SoundEngine soundEngine;
    private float wheelRadius;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        motor = GetComponent<Motor>();
        transmissao = GetComponent<Transmsition>();
        steering = GetComponent<Steering>();
        brake = GetComponent<Brake>();
        soundEngine = GetComponent<SoundEngine>();
        transmissao.ChangeGear(0);
        wheelRadius = steering.wheels[0].radius;

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
        steering.SteeringAmount(steringAmount);
        motor.BackTorqueDistribuition = (0.5f + (-(steering.Amount * backTorqueDistribuitionSteering )/ steering.maxAngle) / 2);
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

