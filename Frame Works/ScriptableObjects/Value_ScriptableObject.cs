using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Value_ScriptableObject<T> : ScriptableObject
{
    [SerializeField]
    protected T value;
    public T Value {get => GetValue();}
    protected virtual T GetValue() => value;
}


