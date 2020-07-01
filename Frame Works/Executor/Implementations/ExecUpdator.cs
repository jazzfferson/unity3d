
using System;

namespace JazzDev.Executor
{
    public class ExecUpdator : Executor<ExecUpdator>
    {
        private bool endedDelay = false;

        public ExecUpdator(/*ExecutorParameters parameters,*/ EUpdateType updateType = EUpdateType.Normal) : base(/*parameters,*/ updateType)
        {
            this.executorType.Value = "ExecUpdator";
        }
        public ExecUpdator(string id="Updator", float initialDelay=0, EUpdateType updateType = EUpdateType.Normal) : base(/*new ExecutorParameters(id, initialDelay)*/updateType)
        {
            this.id.Value = id;
            this.defaultDelay.Value = initialDelay;
            this.executorType.Value = "ExecUpdator";
        }
        public override void Reset()
        {
            base.Reset();
            endedDelay = false;
        }

        public override void Update(float time)
        {
            if (delayTimer >= 0)
            {
                delayTimer -= time;
                if (delayTimer <= 0) { endedDelay = true; }
            }
            else if (endedDelay)
            {
                base.Update(time);
            }
        }
    }
}
