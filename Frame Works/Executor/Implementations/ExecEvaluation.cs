
using System;
using UnityEngine;

namespace JazzDev.Executor
{
    public class ExecEvaluation : ExecutorLoopable<ExecEvaluation>
    {
        private bool endedDelay = false;
        private float time;
        public float Time=>time;
        protected FloatParam duration = new FloatParam("Duration",0);
        public float Duration =>duration.Value;
       // public float Duration => parameters.duration;

       protected DirectionParam direction = new DirectionParam("Direction", EExecutorDirection.Forward);

        protected float internalEvaluation;
        protected float evaluationWithDirection;
        public float Evaluation => evaluationWithDirection;

        public ExecEvaluation(/*ExecutorParameters parameters,*/ EUpdateType updateType = EUpdateType.Normal) : base(/*parameters, */updateType)
        {
             this.parametersList.Add(this.duration);
             this.executorType.Value = "ExecEvaluation";
        }
        public ExecEvaluation(float duration = 1, float delay = 0,EExecutorDirection direction = EExecutorDirection.Forward,string id = "Timer", EUpdateType updateType = EUpdateType.Normal) : base(updateType)
        {
            this.parametersList.Add(this.duration);
            this.executorType.Value = "ExecEvaluation";
           // new ExecutorParameters() { id = id, defaultDelay = delay, duration = duration }, updateType

            this.id.Value = id;
            this.defaultDelay.Value = delay;
       
            //this.parameters.duration = duration;
            this.duration.Value = duration;
           // this.parameters.direction = direction;
           this.direction.Value = direction;
           
        }
        public override void Reset()
        {
            time = 0;
            endedDelay = false;
            base.Reset();
        }
        public override ExecutorBase Run()
        {
            time = 0;
            internalEvaluation = 0;
            endedDelay = false;
            return base.Run();
        }
        protected override void RunNewLoop()
        {
            actualLoop++;
            InvokeOnIterateEvent();
            Run();
        }
        public override void Update(float deltaTime)
        {
            if (delayTimer >= 0)
            {
                delayTimer -= deltaTime;
                if (delayTimer <= 0) { endedDelay = true; InvokeOnStartCallBack();}
            }
            else if (endedDelay)
            {
                time += deltaTime;
               // internalEvaluation = Mathf.Clamp01(time / parameters.duration);
               internalEvaluation = Mathf.Clamp01(time / duration.Value);
               // evaluationWithDirection = parameters.direction == EExecutorDirection.Forward?internalEvaluation:1-internalEvaluation;
               evaluationWithDirection = direction.Value == EExecutorDirection.Forward?internalEvaluation:1f-internalEvaluation;

                base.Update(deltaTime);

                if (internalEvaluation >= 1)
                {
                    //if ((parameters.loops > 0 || parameters.loops == -1) && actualLoop < parameters.loops)
                    if ((loops.Value > 0 || loops.Value == -1) && actualLoop < loops.Value)
                    {
                        //hasEnded = true;
                        hasEnded.Value = true;
                        RunNewLoop();
                    }
                    else
                    {
                        
                        //hasEnded = true;
                          hasEnded.Value = true;
                        InvokeOnFinishCallBack();
                       // if (parameters.destroyOnFinish) { Destroy(false); }
                       if (destroyOnFinish.Value) { Destroy(false); }
                    }

                }
            }
        }
        public ExecEvaluation SetDuration(float duration)
        {
            //parameters.duration = duration;
            this.duration.Value = duration;
            return this;
        }


        public void SetEvaluation(float evaluation)
        {
            internalEvaluation = Mathf.Clamp01(evaluation);
            time =  internalEvaluation * duration.Value;
        }
        public ExecEvaluation SetDirection(EExecutorDirection direction)
        {
            //this.parameters.direction = direction;
            this.direction.Value = direction;
            return this;
        }
    }
}
