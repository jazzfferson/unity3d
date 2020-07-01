
using System;

namespace JazzDev.Executor
{
    public abstract class ExecutorOnce<T> : ExecutorLoopable<T> where T : ExecutorBase
    {
        public ExecutorOnce(float delay = 0, string id = "ExecutorOnce", EUpdateType updateType = EUpdateType.Normal) : base(/*new ExecutorParameters(id, delay), */updateType)
        {
            this.id.Value = id;
            this.defaultDelay.Value = delay;
        }

        public override void Update(float deltaTime)
        {
            if (delayTimer >= 0)
            {
                delayTimer -= deltaTime;

                if (delayTimer <= 0)
                {
                    base.Update(deltaTime);

                    //if (parameters.loops > 0 || parameters.loops == -1)
                    if (loops.Value > 0 || loops.Value == -1)
                    {
                        //if (actualLoop >= parameters.loops)
                        if(actualLoop>=loops.Value &&  loops.Value != -1)
                        {
                            // hasEnded = true;
                            hasEnded.Value = true;
                            InvokeOnFinishCallBack();
                            //if (parameters.destroyOnFinish) { Destroy(false); }
                            if (destroyOnFinish.Value) { Destroy(false); }
                        }
                        else
                        {
                           // InvokeOnFinishCallBack();
                            RunNewLoop();
                        }
                    }
                    else
                    {
                        // hasEnded = true;
                        hasEnded.Value = true;
                        InvokeOnFinishCallBack();
                        //if (parameters.destroyOnFinish) { Destroy(false); }
                        if (destroyOnFinish.Value) { Destroy(false); }
                    }
                }
            }
        }
    }
}
