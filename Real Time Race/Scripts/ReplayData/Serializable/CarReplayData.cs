using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class CarReplayData : BaseReplayData {
    
   
    public LapInfo[] lapInfos;
    public int totalFrames;
    public float bestTime;
    public float totalTime;
    public int bestLap;

    //Variáveis utilizadas em runtime
    private List<BodyFrame> _body;
    private List<InputFrame> _input;
    private List<LapInfo> _lapInfos;

    public CarReplayData()
    {
        _body = new List<BodyFrame>();
        _input = new List<InputFrame>();
        _lapInfos = new List<LapInfo>();
    }

    /// <summary>
    /// Numero de voltas feita nas pistas
    /// </summary>
    public int NumberOfLaps
    {
        get { return lapInfos.Length; }
    }

    /// <summary>
    /// Adiciona frames a lista de body e input frames
    /// </summary>
    /// <param name="bodyFrame"></param>
    /// <param name="inputFrame"></param>
    public void AddBodyInputFrame(BodyFrame bodyFrame, InputFrame inputFrame)
    {
        _body.Add(bodyFrame);
        _input.Add(inputFrame);
    }

    /// <summary>
    /// Adiciona dois valores. Um para o primeiro frame da volta e outro para o ultimo frame da volta.
    /// </summary>
    /// <param name="framesPerlap"></param>
    public void AddLapInfo(int firtFrame,int lastFrame, float lapTime)
    {
        _lapInfos.Add(new LapInfo(firtFrame, lastFrame, lapTime));
    }

    /// <summary>
    /// Prepara a classe para ser serializada
    /// </summary>
    public void ToSerialize()
    {
        body = _body.ToArray();
        input = _input.ToArray();
        lapInfos = _lapInfos.ToArray();
        totalFrames = Length();
    }

    
}

