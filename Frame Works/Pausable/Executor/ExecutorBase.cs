using UnityEngine;
using System.Collections;

namespace JazzDev.Executor
{
    public abstract class ExecutorBase : IPausable
    {
        private static ExecutorManager m_TimerManager;
        private static void Initialize()
        {
            GameObject obj = new GameObject("Executor_Function_TimerManager ", typeof(ExecutorManager));
            m_TimerManager = obj.GetComponent<ExecutorManager>();
        }

        public static ExecutorManager GetManager()
        {
            if (m_TimerManager == null) { Initialize(); }
            return m_TimerManager;
        }

        protected ExecutorParameters parameters;
        public ExecutorParameters Parameters => parameters;
        protected float timer = 0;

        protected bool ended = true;
        protected bool paused;
        protected EUpdateType updateType;

        public bool Paused { get => paused; set => paused = value; }
        public EUpdateType UpdateType { get => updateType; }
        public string Id { get => parameters.id; }

        //this.onExecute = onExecute;
        public ExecutorBase(ExecutorParameters parameters, EUpdateType updateType = EUpdateType.Normal)
        {
            this.updateType = updateType;
            this.parameters = parameters;
            ExecutorBase.GetManager().Add(this);
        }
        protected virtual void SetDefaultValues()
        {
            ended = false;
            timer = parameters.defaultDelay;
        }
        public virtual void Destroy(bool callOnFinishCallBack)
        {
            GetManager().Destroy(this);
        }
        public abstract void Update(float time);
    }
}
