using System;
using UnityEngine;

namespace JazzDev.Executor
{
    public abstract class Executor<T> : ExecutorBase where T : ExecutorBase
    {
        protected ExecutorBase instance;

        public event Action<T> OnStartEvent;
        public Action<T> OnStartDelegate;

        public event Action<T> OnUpdateEvent;
        public Action<T> OnUpdateDelegate;

        public event Action<T> OnFinishEvent;
        public Action<T> OnFinishDelegate;

        public event Action<T> OnDestroyEvent;
        public Action<T> OnDestroyDelegate;

        public Executor(/*ExecutorParameters parameters,*/ EUpdateType updateType) : base(/*parameters,*/ updateType)
        {
            instance = this;
        }
        public sealed override U Run<U>()
        {
            Run();
            return (U)instance;
        }
        public override ExecutorBase Run()
        {
            // isAlive = true;
            isAlive.Value = true;
            //  hasEnded = false;
            hasEnded.Value = false;
            Pause = false;
            delayTimer = GetDelay();
            return this;
        }
        public override void Reset()
        {
            //   hasEnded = false;
            //    isAlive = false;
            isAlive.Value = false;
            hasEnded.Value = false;

            delayTimer = GetDelay();
            Pause = false;
        }
        /// <summary>
        /// Por padrão o executor se autoDestroy após a execução
        /// </summary>
        /// <param name="destroyOnFinish"></param>
        /// <returns></returns>
        public T SetDestroy(bool destroyOnFinish)
        {
            //parameters.destroyOnFinish = destroyOnFinish;
            this.destroyOnFinish.Value = destroyOnFinish;
            return (T)instance;
        }
        public T SetKeepBetweenScenes(bool keepBetweenScenes)
        {
            // parameters.keepBetweenScenes = keepBetweenScenes;
            this.keepBetweenScenes.Value = keepBetweenScenes;
            return (T)instance;
        }
        public T SetDelay(float delay)
        {
            // parameters.defaultDelay = delay;
            this.defaultDelay.Value = delay;
            return (T)instance;
        }
        public T SetId(string id)
        {
            // parameters.id = id;
            this.id.Value = id;
            return (T)instance;
        }
        public override void Destroy(bool callOnFinishCallBack = false)
        {
            if (callOnFinishCallBack) { InvokeOnFinishCallBack(); }
            OnDestroyDelegate?.Invoke((T)instance);
            OnDestroyEvent?.Invoke((T)instance);

            OnStartEvent = null;
            OnStartDelegate = null;

            OnUpdateEvent = null;
            OnUpdateDelegate = null;

            OnFinishEvent = null;
            OnFinishDelegate = null;

            OnDestroyEvent = null;
            OnDestroyDelegate = null;

            base.Destroy(callOnFinishCallBack);
        }
        public override void Update(float deltaTime)
        {
            OnUpdateDelegate?.Invoke((T)instance);
            OnUpdateEvent?.Invoke((T)instance);
        }
        protected virtual float GetDelay()
        {
            // return parameters.defaultDelay;
            return defaultDelay.Value;
        }
        protected virtual void InvokeOnFinishCallBack()
        {
            OnFinishDelegate?.Invoke((T)instance);
            OnFinishEvent?.Invoke((T)instance);
        }
        protected virtual void InvokeOnStartCallBack()
        {
            OnStartDelegate?.Invoke((T)instance);
            OnStartEvent?.Invoke((T)instance);
        }
        protected virtual void InvokeOnUpdateCallBack()
        {
            OnUpdateDelegate?.Invoke((T)instance);
            OnUpdateEvent?.Invoke((T)instance);
        }

        public T SetStart(Action<T> callback)
        {
            this.OnStartDelegate = callback;
            return (T)instance;
        }

        public T SetUpdate(Action<T> callback)
        {
            this.OnUpdateDelegate = callback;
            return (T)instance;
        }

        public T SetFinish(Action<T> callback)
        {
            this.OnFinishDelegate = callback;
            return (T)instance;
        }

        public T SetDestroy(Action<T> callback)
        {
            OnDestroyDelegate = callback;
            return (T)instance;
        }
    }

}

