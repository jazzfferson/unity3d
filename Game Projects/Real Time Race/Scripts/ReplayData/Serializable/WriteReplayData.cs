using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WriteReplayData
{

    private CarReplayData _scriptableReplayData;
    private int frames;
    private int lastLapFrame;
    public int Frames
    {
        get { return frames; }
    }


    public WriteReplayData()
    {
        Debug.Log("Write Replay Data Constructor inicialized");
        _scriptableReplayData = new CarReplayData();
        frames = 0;

    }
    public void AddFrame(BodyFrame body,InputFrame input)
    {
        _scriptableReplayData.AddBodyInputFrame(body, input);
        frames++;
    }
    /// <summary>
    /// Registra em que frame a volta começou
    /// </summary>
    public void NewLap(float lapTime)
    {
        Debug.Log("New lap frame add : first = "+lastLapFrame+" last = " + frames);
        _scriptableReplayData.AddLapInfo(lastLapFrame, frames, lapTime);
        lastLapFrame = frames;
    }
    public CarReplayData CreateScriptableData()
    {
        Debug.Log("Write Replay Data Created Scriptable Data");

        _scriptableReplayData.ToSerialize();

        return _scriptableReplayData;
    }
}

