using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace JazzDev.Executor
{
    public class SequencerItem
    {
        private ExecutorBase executor;
        private float delay;
        public SequencerItem(ExecutorBase executor, float delay)
        {
            this.delay = delay;
            this.executor = executor;
        }
        public ExecutorBase Executor { get => executor; }
        public float Delay { get => delay; }
    }
    public class Sequencer : ExecutorLoopable<Sequencer>
    {
        protected List<SequencerItem> itemList;
        protected SequencerItem actualItem;
        public SequencerItem ActualItem => actualItem;
        private int itemIndex;
        public int ActualItemIndex => itemIndex;
        private float itemDelay = -1;
        public event Action<Sequencer> OnItemFinishEvent;
        public event Action<Sequencer> OnItemBeginEvent;

        // public Sequencer(float delay = 0f, string id = "Sequencer") : base(new ExecutorParameters(id, delay), EUpdateType.Normal) { InitializeList(); }
        public Sequencer(float delay = 0f, string id = "Sequencer") : base(EUpdateType.Normal)
        {
            this.defaultDelay.Value = delay;
            this.id.Value = id;
            InitializeList();
            this.executorType.Value = "Sequencer";
        }
       // public Sequencer(ExecutorParameters parameters) : base(parameters, EUpdateType.Normal) { InitializeList(); }
        public Sequencer() : base(/*new ExecutorParameters(),*/ EUpdateType.Normal) { InitializeList(); this.executorType.Value = "Sequencer";}
        private void InitializeList()
        {
            itemList = new List<SequencerItem>();
        }

        public Sequencer Add<T>(ExecutorLoopable<T> executor, float delay = 0f) where T : ExecutorLoopable<T>
        {
            OnPauseChangeCallBack += Sequencer_OnPauseChangeCallBack;
            SequencerItem newItem = new SequencerItem(executor, delay);
            itemList.Add(newItem);
            executor.OnFinishEvent += this.Executor_OnFinishEvent1;
            executor.SetDestroy(false);
            executor.OnDestroyEvent += Executor_OnDestroyEvent;
            return this;
        }

        private void Executor_OnFinishEvent1<T>(T obj) where T : ExecutorLoopable<T>
        {
           
           // if (obj.ActualLoop >= obj.Parameters.loops)
           if (obj.ActualLoop >= obj.Loops.Value)
            {
                OnItemFinishEvent?.Invoke(this);
                JumpToNextItem(obj);
            }
        }

        private void Sequencer_OnPauseChangeCallBack(ExecutorBase obj)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                // itemList[i].Executor.Pause = pause;
                itemList[i].Executor.Pause = pause.Value;
            }
        }

        private void Executor_OnDestroyEvent(ExecutorBase obj)
        {
            itemList.RemoveAll(item => item.Executor.Equals(obj));
            JumpToNextItem(obj);
        }
        private void JumpToNextItem(ExecutorBase obj)
        {
            if (HasNewItem())
            {
                itemIndex++;
                actualItem = itemList[itemIndex];
                SetActualItemDelay();
            }
            else
            {
                //if (actualLoop >= parameters.loops)
                if (actualLoop >= this.loops.Value)
                {
                    // hasEnded = true;
                    hasEnded.Value = true;
                    InvokeOnFinishCallBack();
                   // if (parameters.destroyOnFinish) { Destroy(false); }
                   if (this.destroyOnFinish.Value) { Destroy(false); }
                }
                else
                {
                    InvokeOnFinishCallBack();
                    RunNewLoop();
                }

            }
        }
        public override ExecutorBase Run()
        {
            SetInitialState();
            return base.Run();
        }
        protected override void RunNewLoop()
        {
            SetInitialState();
            base.RunNewLoop();
        }

        public override void Reset()
        {
            SetInitialState();
            base.Reset();
        }

        private void SetInitialState()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].Executor.Reset();
            }
            itemIndex = 0;
            actualItem = itemList[itemIndex];
            SetActualItemDelay();
        }
        private void SetActualItemDelay()
        {
            itemDelay = actualItem.Delay;
        }

        private bool HasNewItem()
        {
            return itemList.Count - 1 >= itemIndex + 1;
        }

        public override void Update(float deltaTime)
        {
            if (itemDelay >= 0)
            {
                itemDelay -= deltaTime;

                if (itemDelay <= 0)
                {
                    itemDelay = -1;
                    actualItem.Executor.Run();
                    OnItemBeginEvent?.Invoke(this);
                }
            }
            base.Update(deltaTime);
        }

        public Sequencer OnItemFinish(Action<Sequencer> callBack)
        {
            OnItemFinishEvent += callBack;
            return this;
        }
        public Sequencer OnItemBegin(Action<Sequencer> callBack)
        {
            OnItemBeginEvent += callBack;
            return this;
        }
    }
}

