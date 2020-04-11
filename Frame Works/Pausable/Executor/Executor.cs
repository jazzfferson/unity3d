using System;

namespace JazzDev.Executor
{
    public abstract class Executor<T> : ExecutorBase where T : ExecutorBase
    {
        protected Action<T> executeCallBack;
        protected Action<T> onFinishCallBack;
        private ExecutorBase instance;
        public Executor(ExecutorParameters parameters,EUpdateType updateType) : base(parameters, updateType)
        {
            instance = this;
        }    
        public T SetOnExecuteCallBack(Action<T> executeCallBack)
        {
            this.executeCallBack = executeCallBack;
            return (T)instance;
        }
        public T Execute()
        {
            SetDefaultValues();
            return (T)instance;
        }
        public T SetAutoDestroy(bool autoDestroy)
        {
            parameters.autoDestroy = autoDestroy;
            return (T)instance;
        }
        public T SetDelay(float delay)
        {
            parameters.defaultDelay = delay;
            return (T)instance;
        }
        public T SetId(string id)
        {
            parameters.id = id;
            return (T)instance;
        }
        public override void Destroy(bool callOnFinishCallBack)
        {
            ended = true;
            if (callOnFinishCallBack) {onFinishCallBack?.Invoke((T)instance);}
            base.Destroy(callOnFinishCallBack);
        }
    }
    
    
}

