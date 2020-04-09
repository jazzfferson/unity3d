using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum EChainType : int { ChainTypeHorizontal, ChainTypeVertical};

public class Chain
{
    public List<Candy> arrayCandy=new List<Candy>();
    public EChainType chainType;
    public int score;

    public void AddCandy(Candy candy)
    {
        if(candy!=null)
        arrayCandy.Add(candy);
    }
}
