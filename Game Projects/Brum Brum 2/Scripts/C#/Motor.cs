using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Motor : MonoBehaviour
{
    //Variavel Para controle de torque !!! Apenas para teste.
    public float torqueAdjust;
    public WheelCollider[] frontWheels, backWheels;
    public AnimationCurve torqueCurve;
    public float limitRpmRecoveryTime = 0.1f;
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
    public float FrontRightLeftTorqueDistribuition
    {
        get;
        set;
    }
    /// <summary>
    /// Distribuição de torque entres as rodas esquerda e direita
    /// </summary>
    public float BackRightLeftTorqueDistribuition
    {
        get;
        set;
    }

    /// <summary>
    /// Distribuição de torque entre as rodas da frente e traseira
    /// </summary>
    public float FrontRearTorqueDistribution;

    private delegate void TorqueDelegate(float torque);
    private List<TorqueDelegate> torqueDelegateList;
    private float minVariacaoRpm = 50;
    private float maxVariacaoRpm = 120;
    private int wheels;
    private float frontTorqueR, frontTorqueL, backTorqueR, backTorqueL;
    private List<WheelCollider> allWheels;
    private int reverse = 1;
    private float limitRecoveryTimeTimer;
    private bool isRecovering = false;

    void Awake()
    {
        if (frontWheels != null)
            wheels += frontWheels.Length;
        if (backWheels != null)
            wheels += backWheels.Length;

        allWheels = new List<WheelCollider>();
        torqueDelegateList = new List<TorqueDelegate>();

        if (frontWheels != null)
        {
            if (frontWheels.Length == 2)
            {

                allWheels.Add(frontWheels[0]);
                allWheels.Add(frontWheels[1]);
                torqueDelegateList.Add(new TorqueDelegate(AddFrontTorque));
            }
        }
        if (backWheels != null)
        {
            if (backWheels.Length == 2)
            {

                allWheels.Add(backWheels[0]);
                allWheels.Add(backWheels[1]);
                torqueDelegateList.Add(new TorqueDelegate(AddBackTorque));
            }
        }

        FrontRightLeftTorqueDistribuition = 0.5f;
        BackRightLeftTorqueDistribuition = 0.5f; 
    }

    private float MotorRpm(float gearRatio, float finalDrive)
    {

        float wheelsRpm = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            wheelsRpm += wheel.rpm;
        }
        wheelsRpm = wheelsRpm / allWheels.Count;

        return wheelsRpm * gearRatio * finalDrive * reverse;
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
        /* foreach (TorqueDelegate delega in torqueDelegateList)
         {
             delega(torque / torqueDelegateList.Count);
         }*/

        if (frontWheels != null)
        {
            if(frontWheels.Length>0)
            AddFrontTorque(Mathf.Lerp(0, torque,FrontRearTorqueDistribution));
        }
        if(backWheels!=null)
        {
            if (backWheels.Length > 0)
                AddBackTorque(Mathf.Lerp(torque,0, FrontRearTorqueDistribution));
        }

    }
    public void MotorUpdate(float gearRatio, float finalDrive, float throttle)
    {
        if (!isRecovering)
        {
            Throttle = throttle;
        }
        else
        {
            Throttle = 0;
        }


        ActualRpm = Mathf.Clamp(MotorRpm(gearRatio, finalDrive), minMotorRpm, maxMotorRpm);
        if (ActualRpm >= maxMotorRpm) { isRecovering = true; }



        ActualTorque = MotorTorque(ActualRpm, gearRatio, finalDrive, throttle);
        ApplyTorque(ActualTorque);

        if (isRecovering)
        {
            limitRecoveryTimeTimer += Time.fixedDeltaTime;

            if (limitRecoveryTimeTimer >= limitRpmRecoveryTime)
            {
                limitRecoveryTimeTimer = 0;
                isRecovering = false;
            }
        }
    }
    public float CheckMotorRpm(float gearRatio, float finalDrive)
    {
        return MotorRpm(gearRatio, finalDrive);
    }

    private void AddFrontTorque(float torque)
    {
        
        frontTorqueR = Mathf.Lerp(0, torque, FrontRightLeftTorqueDistribuition);
        frontTorqueL = Mathf.Lerp(torque, 0, FrontRightLeftTorqueDistribuition);


        frontWheels[0].motorTorque = frontTorqueR * reverse;
        frontWheels[1].motorTorque = frontTorqueL * reverse;

    }
    private void AddBackTorque(float torque)
    {
        backTorqueR = Mathf.Lerp(0, torque, BackRightLeftTorqueDistribuition);
        backTorqueL = Mathf.Lerp(torque, 0, BackRightLeftTorqueDistribuition);


        backWheels[0].motorTorque = backTorqueR * reverse;
        backWheels[1].motorTorque = backTorqueL * reverse;
    }
    public void SetReverse(bool isReverse)
    {
        reverse = isReverse ? -1 : 1;
    }
}
