using UnityEngine;
using System.Collections;
using System;

public enum ECandyType:int {One=1,Two=2,Three=3,Four=4,Five=5,Six=6,Null=0};

public class Candy : Spritable {

    public const int NumCandyTypes = 6;
    public int Column;
    public int Row;
    public ECandyType type = ECandyType.Null; 
}
