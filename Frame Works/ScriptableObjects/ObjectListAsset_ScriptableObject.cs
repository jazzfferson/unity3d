using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectListAsset_ScriptableObject<T> : ScriptableObject
{
    [SerializeField]
    private List<T> elementsList = new List<T>();
    public List<T> ElementsList { get => elementsList; }

    public void AddElement(T element)
    {
        if(elementsList.Contains(element))return;
        elementsList.Add(element);
    }
    public void RemoveElement(T element)
    {
        if(!elementsList.Contains(element))return;
        elementsList.Remove(element);
    }
}
