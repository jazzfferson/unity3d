
using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;


namespace JazzDev.Executor
{
    public abstract class ExecutorBase : IPausable<ExecutorBase>
    {      
        private static ExecutorManager m_TimerManager;
        private static void Initialize()
        {  
            GameObject obj = new GameObject("_ExecutorManager_", typeof(ExecutorManager));
            m_TimerManager = obj.GetComponent<ExecutorManager>();
        }
        public static ExecutorManager GetManager()
        {
            if(ExecutorManager._applicationIsQuitting)return null;
            if (m_TimerManager == null) { Initialize(); }
            return m_TimerManager;
        }
        //   protected ExecutorParameters parameters;
        //   public ExecutorParameters Parameters => parameters;

        //protected bool pause;
        //public bool Pause { get { return pause; } set { pause = value; OnPauseChangeCallBack?.Invoke(this); } }
        protected BoolParam pause = new BoolParam("Pause", false);
        public bool Pause { get { return pause.Value; } set { pause.Value = value; OnPauseChangeCallBack?.Invoke(this); } }
        public event Action<ExecutorBase> OnPauseChangeCallBack;



        protected StringParam executorType = new StringParam("ExecutorType", "ExecutorBase");


        protected FloatParam defaultDelay = new FloatParam("DefaultDelay", 0);

        protected float delayTimer = 0;
        // protected bool isAlive = false;
        protected BoolParam isAlive = new BoolParam("IsAlive", false);
        // protected bool hasEnded = false;
        protected BoolParam hasEnded = new BoolParam("HasEnded", false);
        //public bool HasEnded => hasEnded;
        public bool HasEnded => hasEnded.Value;
        //public bool IsAlive => isAlive;
        public bool IsAlive => isAlive.Value;

        // public readonly EUpdateType updateType;
        public readonly UpdateTypeParam updateType = new UpdateTypeParam("UpdateType", EUpdateType.Normal);

        protected StringParam id = new StringParam("Id", "");
        public string Id { get => id.Value; }
        // public string Id { get => parameters.id; }


        protected BoolParam keepBetweenScenes = new BoolParam("KeepBetweenScenes", false);
        public BoolParam KeepBetweenScenes { get => keepBetweenScenes; }

        protected BoolParam destroyOnFinish = new BoolParam("DestroyOnFinish", true);

        protected List<IParameter> parametersList = new List<IParameter>();
        public List<IParameter> ParametersList => parametersList;

        //-Executor
        //Id
        //Delay
        //Is Alive
        //Has Ended
        //Update Type
        //Pause


        //this.onExecute = onExecute;
        public ExecutorBase(/*ExecutorParameters parameters, */EUpdateType updateType)
        {
            // this.updateType = updateType;
            this.updateType.Value = updateType;
            // this.parameters = parameters;

            this.parametersList.Add(executorType);
            this.parametersList.Add(id);
            this.parametersList.Add(defaultDelay);
            this.parametersList.Add(destroyOnFinish);
            this.parametersList.Add(isAlive);
            this.parametersList.Add(hasEnded);
            this.parametersList.Add(pause);
            this.parametersList.Add(keepBetweenScenes);
            this.parametersList.Add(this.updateType);

            GetManager().Add(this);
        }
      
        public virtual void Destroy(bool callOnFinishCallBack)
        {
            OnPauseChangeCallBack = null;
            GetManager()?.Destroy(this);
        }

        public abstract void Reset();
        public abstract void Update(float deltaTime);
        public abstract T Run<T>() where T : ExecutorBase;
        public abstract ExecutorBase Run();
        

    }
}
