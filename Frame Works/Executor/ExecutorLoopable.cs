using UnityEngine;
using System.Collections;
using System;

namespace JazzDev.Executor
{
    public abstract class ExecutorLoopable<T> : Executor<T>, IIterateEvent<T> where T : ExecutorBase
    {
        protected int actualLoop;

        public event Action<T> OnIterateEvent;
        public Action<T> OnIterateDelegate;

        public int ActualLoop=>actualLoop;

        protected IntParam loops = new IntParam("Loops", 0);
        public IntParam Loops => loops;

        public ExecutorLoopable(/*ExecutorParameters parameters, */EUpdateType updateType = EUpdateType.Normal) : base(/*parameters,*/ updateType)
        {
           this.parametersList.Add(loops);
        }
        /// <summary>
        /// Repete o executor do começo
        /// </summary>
        /// <param name="iterations">O numero de repetições. -1 loop infinito</param>
        /// <returns></returns>
        public T SetLoop(int loops)
        {
            this.loops.Value = loops;
            //parameters.loops = loops;
            return (T)instance;
        }

        public override ExecutorBase Run()
        {
            actualLoop = 0;
            return base.Run();
        }
        public override void Reset()
        {
            actualLoop = 0;
            base.Reset();
        }
        public override void Destroy(bool callOnFinishCallBack = false)
        {
            OnIterateEvent = null;
            OnIterateDelegate = null;
            base.Destroy(callOnFinishCallBack);
        }
        protected virtual void RunNewLoop()
        {
            actualLoop++;
            InvokeOnIterateEvent();
            base.Run();
        }
        protected virtual void InvokeOnIterateEvent()
        {
            OnIterateDelegate?.Invoke((T)instance);
            OnIterateEvent?.Invoke((T)instance);
        }
        public T SetIterate(Action<T> callback)
        {
             this.OnIterateDelegate=callback;
             return (T)instance;
        }
    }
}
