
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CarReplay : MonoBehaviour
{
    MobileCarController axiscarController;
    Axles axles;
    Wheel[] rodas;
    CarDynamics carDinamics;
    Rigidbody my_RigidBody;

    static string replayName;
    bool playReplay = false;
    int frame;


    float sampleSize;
    private WriteReplayData _writeReplayData;
    private ReadReplayData _readReplayData;

    public ReadReplayData ReadReplayData
    {
        get { return _readReplayData; }
    }
    public WriteReplayData WriteReplayData
    {
        get { return _writeReplayData; }
    }



    void Start()
    {
        axiscarController = GetComponent<MobileCarController>();
        axles = GetComponent<Axles>();
        carDinamics = GetComponent<CarDynamics>();
        rodas = axles.allWheels;
        my_RigidBody = GetComponent<Rigidbody>();

        replayName = "Replay_" + RacePrepare.pista.ToString() + "_" + RacePrepare.carro;

        if (ReplayControlGui.Replaying)
        {            
            playReplay = true;
            Play();
        }
        else
        {
            Record();
        }

        print(Application.persistentDataPath);
    }

    void ReplayData_OnReplayFinished(object sender, EventArgs e)
    {
        LoadScreen.instance.LoadLevel(0);
        ReplayControlGui.Replaying = false;
    }

    void FixedUpdate()
    {
        //O FixedUpdate está sendo atualizado á 100Hz. 
        //Então só é gravado ou lido uma informação á cada 50hz ou metade deste ciclo
        // Isso poupa processamento e diminue o tamanho do arquivo do replay

       // if (++frame%2==0)
       ReadAndWriteData();
    }
    void ReadAndWriteData()
    {
        if (ReplayControlGui.Replaying && playReplay)
        {

            BodyFrame body;
            InputFrame input;

            _readReplayData.ReadFrame(out body, out input);

            axiscarController.acceReplay = input._acelerador;
            axiscarController.bracReplay = input._freio;
            axiscarController.steeReplay = input._direcao;

            my_RigidBody.MovePosition(body.GetPosition());
            my_RigidBody.MoveRotation(body.GetRotation());
            my_RigidBody.velocity = body.GetVelocity();
            my_RigidBody.angularVelocity = body.GetAngularVelocity();

            if (input._transmissao == 1)
            {
                axiscarController.ShiftUp();
            }
            else if (input._transmissao == -1)
            {
                axiscarController.ShiftDown();
            }

        }
        else
        {
            if (_writeReplayData == null)
                return;

            _writeReplayData.AddFrame(new BodyFrame(my_RigidBody), new InputFrame(axiscarController.stee, axiscarController.acce, axiscarController.brac, axiscarController.transmission));
            axiscarController.transmission = 0;
           

        }
    }
    void Record()
    {
        axiscarController.Recording = true;
        print("Recording");
        _writeReplayData = new WriteReplayData();
    }
    void Play()
    {
        axiscarController.Recording = false;
        print("Playing");

        //Le replay salvo no disco
        //_readReplayData = new ReadReplayData(SaveAndLoadReplayAsset.LoadReplayAsset(replayName));

        //Le replay salvo na memória através de uma variável statica
        _readReplayData = new ReadReplayData(SaveAndLoadReplayData.LoadStaticReplay());

        _readReplayData.OnFinishedReplay += new EventHandler(ReplayData_OnReplayFinished);        
    }
    void Replay()
    {
        //Salva replay no disco
        // SaveAndLoadReplayAsset.SaveReplayAsset(_writeReplayData.CreateScriptableData(), replayName);
        
        //Sava o replay em uma variável estática para permanecer na memória ao trocar de cena.
        SaveAndLoadReplayData.SaveStaticReplay(_writeReplayData.CreateScriptableData());
        Restart();
    }

    public void PlayReplay()
     {   
         Replay();
     } 
    public void Restart()
     {
         ReplayControlGui.Replaying = true;
         print(RacePrepare.pista.ToString());
         LoadScreen.instance.LoadLevel(RacePrepare.pista.ToString());
     }
    public void RestartRace()
    {
        ReplayControlGui.Replaying = false;
        LoadScreen.instance.LoadLevel(RacePrepare.pista.ToString());
    }
    public void SetLapInfo(float lapTime)
    {
        _writeReplayData.NewLap(lapTime);
    }
   
}




