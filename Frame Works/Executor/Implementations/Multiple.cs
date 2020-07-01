using System;

namespace JazzDev.Executor
{
    public class Multiple : Executor<Multiple>
    {
        private int actualIteration;
        public int ActualIteration => actualIteration;
        public Multiple(EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(),updateType)
        {
        }
        public Multiple(ExecutorParameters parameters,EUpdateType updateType = EUpdateType.Normal) : base(parameters, updateType)
        {
        }
        public Multiple(string id, float defaultDelay,int iterations,EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(id, defaultDelay, false,defaultDelay, iterations), updateType)
        {
        }
        public Multiple(string id, float defaultDelay,bool isRandomDelay,float maxDelay,int iterations,EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(id, defaultDelay, isRandomDelay,maxDelay, iterations),updateType)
        {
        }
        public Multiple SetIterations(int iterations)
        {
            parameters.iterations = iterations;
            return this;
        }
        public Multiple SetRandomDelay(bool isRandomDelay)
        {
            parameters.isRandomDelay = isRandomDelay;
            return this;
        }
        public Multiple SetMaxDelay(float maxDelay)
        {
            parameters.maxDelay = maxDelay;
            return this;
        }
        public Multiple SetOnFinishCallBack(Action<Multiple> finishCallBack)
        {
            this.onFinishCallBack = finishCallBack;
            return this;
        }

        public override void Update(float time)
        {

            if (ended || paused)
                return;

            if (timer >= 0)
            {
                timer -= time;

                if (timer <= 0)
                {
                    executeCallBack?.Invoke(this);
                    ExecutionIteration();
                }
            }
        }
        private void ExecutionIteration()
        {
            timer = GetDelay();
            actualIteration++;

            if (parameters.iterations>=0)
            {
                if (actualIteration >= parameters.iterations)
                {
                    ended = true;
                    onFinishCallBack?.Invoke(this);
                    if (parameters.autoDestroy) { Destroy(parameters.destroyCallOnFinish); }
                }
            }
        }
        private float GetDelay()
        {
            if (parameters.isRandomDelay)
                return UnityEngine.Random.Range(parameters.defaultDelay, parameters.maxDelay);
            else
                return parameters.defaultDelay;
        }
    }
}
