using UnityEngine;
using System.Collections;
using System;

public class ReadReplayData
{

    private CarReplayData _replayData;
    private int frames;
    private int actualFrame;

    public int Frames
    {
        get { return frames; }
    }
    public int ActualFrame
    {
        get { return actualFrame; }
        set { actualFrame = value; }
    }

    public ReadReplayData(CarReplayData replayData)
    {
        if(replayData!=null)
        Debug.Log("ReadReplayData Constructor inicialized");
        else
       Debug.LogError("ReadReplayData Constructor inicialized with null parameter");

        _replayData = replayData;
        frames = _replayData.input.Length;
    }
    public void ReadFrame(out BodyFrame body, out InputFrame input)
    {
        if (actualFrame < frames - 1)
        {

            body = _replayData.body[actualFrame];           
            input = _replayData.input[actualFrame];
            actualFrame++;         
        }
        else
        {
            if (OnFinishedReplay != null)
            {
                OnFinishedReplay(this, null);
            }

            body = _replayData.body[frames-1];
            input = _replayData.input[frames-1];
        }

    }
    public void ReadFrame(int frame,out BodyFrame body, out InputFrame input)
    {
        body = _replayData.body[frame];
        input = _replayData.input[frame];
    }
    public event EventHandler OnFinishedReplay;

}
