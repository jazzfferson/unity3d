using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JazzDev.Events
{
    [CreateAssetMenu(fileName ="New Void Event", menuName = "ScripTable Events/Void")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Invoke()=>Invoke(new Void());
    }
}
