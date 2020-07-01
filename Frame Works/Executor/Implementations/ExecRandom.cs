using System;

namespace JazzDev.Executor
{
    public class ExecRandom : ExecutorOnce<ExecRandom>
    {
        protected FloatParam maxDelay = new FloatParam("MaxDelay",0);

        public ExecRandom(float delay = 0, float maxDelay = 0, string id = "ExecutorOnce", EUpdateType updateType = EUpdateType.Normal) : base(delay,id,/*new ExecutorParameters(id, delay,maxDelay), updateType*/updateType)
        {
            this.maxDelay.Value = maxDelay;
            this.parametersList.Add(this.maxDelay);
            this.executorType.Value = "ExecRandom";
        }
        public ExecRandom SetRandDelay(float delay, float maxDelay)
        {
           // parameters.defaultDelay = delay;
           this.maxDelay.Value = maxDelay;
           // parameters.maxDelay = maxDelay;
           this.defaultDelay.Value = delay;
            return this;
        }
        protected override float GetDelay()
        {
           // parameters.maxDelay = Math.Max(parameters.defaultDelay, parameters.maxDelay);
          //  return UnityEngine.Random.Range(parameters.defaultDelay, parameters.maxDelay);
           this.maxDelay.Value = Math.Max(this.defaultDelay.Value, this.maxDelay.Value);
           return UnityEngine.Random.Range(this.defaultDelay.Value, this.maxDelay.Value);
        }
    }
}
