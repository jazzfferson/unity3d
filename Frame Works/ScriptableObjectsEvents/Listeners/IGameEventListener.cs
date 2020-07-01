using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JazzDev.Events
{
    public interface IGameEventListener<T>
    {
        void Invoke(T item);
    }
}
