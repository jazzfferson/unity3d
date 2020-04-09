using System;
using System.Collections.Generic;
using UnityEngine;


public class GhostReplayData : BaseReplayData
{
    int actualFrame;

    public GhostReplayData(CarReplayData replayData, int startFrame, int finishFrame)
    {
        body = ChunkOfArray<BodyFrame>(replayData.body, startFrame, finishFrame);
        actualFrame = 0;
    }
  
    /// <summary>
    /// Cria um array novo a partir dos indices passados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param> O array de base para criar o novo array
    /// <param name="start"></param> O indice que irá definir a primeira posição do novo array
    /// <param name="finish"></param> O ultimo indice que ira definir a ultima posicao do array
    /// <returns></returns>
    private T[] ChunkOfArray<T>(T[] array,int start, int finish)
    {  
        int length = finish - start;
        T[] arrayToReturn = new T[length];

        for (int i = 0; i <length; i++)
        {
            arrayToReturn[i] = array[i + start];
        }

        return arrayToReturn;
    }
    /// <summary>
    /// Avança o frame do array e reseta assim que chegar ao fim
    /// </summary>
    public bool NextFrame()
    {
       
        if (actualFrame >= Length()-1)
        {
            actualFrame = 0;
            return false;
        }
        else
        {
            actualFrame++;
            return true;
        }
    }

    public int ActualFrame()
    {
        return actualFrame;
    }

    public Vector3 GetPosicao()
    {
        return GetBodyFrame(actualFrame).GetPosition();
    }

    public Quaternion GetRotacao()
    {
        return GetBodyFrame(actualFrame).GetRotation();
    }
}

