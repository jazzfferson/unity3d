using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Motor : MonoBehaviour
{
    //Variavel Para controle de torque !!! Apenas para teste.
    public float torqueAdjust;
    public WheelCollider[] frontWheels, backWheels;
    public AnimationCurve torqueCurve;
    public int minMotorRpm;
    public int maxMotorRpm;
    public float ActualTorque
    {
        get;
        private set;
    }
    public float ActualRpm
    {
        get;
        private set;
    }
    public float Throttle
    {
        get;
        private set;
    }



    /// <summary>
    /// Distribuição de torque entres as rodas esquerda e direita
    /// </summary>
    public float FrontTorqueDistribuition
    {
        get;
        set;
    }
    /// <summary>
    /// Distribuição de torque entres as rodas esquerda e direita
    /// </summary>
    public float BackTorqueDistribuition
    {
        get;
        set;
    }

    private delegate void TorqueDelegate(float torque);
    private List<TorqueDelegate> torqueDelegateList;

    private float minVariacaoRpm = 50;
    private float maxVariacaoRpm = 120;
    private int wheels;
    private float frontTorqueR, frontTorqueL, backTorqueR, backTorqueL;
    private List<WheelCollider> allWheels;
    



    void Start()
    {
        wheels = frontWheels.Length + backWheels.Length;
        allWheels = new List<WheelCollider>();
        torqueDelegateList = new List<TorqueDelegate>();

        if (frontWheels.Length > 1)
        {
            allWheels.Add(frontWheels[0]);
            allWheels.Add(frontWheels[1]);
            torqueDelegateList.Add(new TorqueDelegate(AddFrontTorque));
        }
        if (backWheels.Length > 1)
        {
            allWheels.Add(backWheels[0]);
            allWheels.Add(backWheels[1]);
            torqueDelegateList.Add(new TorqueDelegate(AddBackTorque));
        }

        FrontTorqueDistribuition = 0.5f;
        BackTorqueDistribuition = 1f;
       
    }

    private float MotorRpm(float gearRatio, float finalDrive)
    {
        
        float wheelsRpm = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            wheelsRpm += wheel.rpm;
        }
        wheelsRpm = wheelsRpm / allWheels.Count;

        return wheelsRpm * gearRatio * finalDrive;
    }

    private float MotorRpmIDE(float throttle)
    {
        float mRpm;

        if (throttle > 0)
            mRpm = Mathf.Clamp((ActualRpm += throttle * maxMotorRpm / 100), Random.Range(minMotorRpm - minVariacaoRpm, minMotorRpm + minVariacaoRpm), Random.Range(maxMotorRpm - maxVariacaoRpm, maxMotorRpm + maxVariacaoRpm));
        else
        {
            mRpm = Mathf.Clamp((ActualRpm -= maxMotorRpm / 200), Random.Range(minMotorRpm - minVariacaoRpm, minMotorRpm + minVariacaoRpm), Random.Range(maxMotorRpm - maxVariacaoRpm, maxMotorRpm + maxVariacaoRpm));
        }

        return mRpm;
    }

    private float MotorTorqueIDE(float motorRpmIde)
    {
        float mTorque;
        mTorque = torqueCurve.Evaluate(motorRpmIde);
        return mTorque;

    }
    private float MotorTorque(float rpm, float gearRatio, float finalDrive, float throttle)
    {
        float mTorque;
        //drive torque = engine_torque * gear_ratio * differential_ratio * transmission_efficiency
        mTorque = (torqueCurve.Evaluate(rpm) * torqueAdjust) * gearRatio * finalDrive * throttle;
        return mTorque;
    }
    private void ApplyTorque(float torque)
    {
        foreach (TorqueDelegate delega in torqueDelegateList)
        {
            delega(torque / torqueDelegateList.Count);
        }
       
    }
    public void MotorUpdate(float gearRatio, float finalDrive, float throttle)
    {
        Throttle = throttle;
        ActualRpm = MotorRpm(gearRatio, finalDrive);
        ActualTorque = MotorTorque(ActualRpm, gearRatio, finalDrive, throttle);
        ApplyTorque(ActualTorque);
    }
    public float CheckMotorRpm(float gearRatio, float finalDrive)
    {
        return MotorRpm(gearRatio, finalDrive);
    }

    private void AddFrontTorque(float torque)
    {
        frontTorqueR = Mathf.Lerp(0, torque, FrontTorqueDistribuition);
        frontTorqueL = Mathf.Lerp(torque, 0, FrontTorqueDistribuition);

       
        frontWheels[0].motorTorque = frontTorqueR;
        frontWheels[1].motorTorque = frontTorqueL;
 
    }
    private void AddBackTorque(float torque)
    {
        backTorqueR = Mathf.Lerp(0, torque, BackTorqueDistribuition);
        backTorqueL = Mathf.Lerp(torque, 0, BackTorqueDistribuition);


        backWheels[0].motorTorque = backTorqueR;
        backWheels[1].motorTorque = backTorqueL;
    }



   
    

}
