using UnityEngine;
using System.Collections.Generic;

namespace JazzDev.Executor
{
    public class ExecutorManager : MonoBehaviour
    {
        [SerializeField]
        private List<ExecutorBase> timersList;
        [SerializeField]
        private List<ExecutorBase> executorsNormalUpdateList;
        [SerializeField]
        private List<ExecutorBase> executorsLateUpdateList;
        [SerializeField]
        private List<ExecutorBase> executorsFixedUpdateList;

        public int ExecutorsCount;

        private void Awake()
        {
            timersList = new List<ExecutorBase>();
            executorsNormalUpdateList = new List<ExecutorBase>();
            executorsLateUpdateList = new List<ExecutorBase>();
            executorsFixedUpdateList = new List<ExecutorBase>();
        }

        public void Add(ExecutorBase executor)
        {
            timersList.Add(executor);

            switch (executor.UpdateType)
            {
                case EUpdateType.Normal:
                    executorsNormalUpdateList.Add(executor);
                    break;
                case EUpdateType.LateUpdate:
                    executorsLateUpdateList.Add(executor);
                    break;
                case EUpdateType.FixedUpdate:
                    executorsFixedUpdateList.Add(executor);
                    break;
                default:
                    break;
            }

            UpdateCount();
        }
        public void Destroy(ExecutorBase functionTimer)
        {
            switch (functionTimer.UpdateType)
            {
                case EUpdateType.Normal:
                    executorsNormalUpdateList.Remove(functionTimer);
                    break;
                case EUpdateType.LateUpdate:
                    executorsLateUpdateList.Remove(functionTimer);
                    break;
                case EUpdateType.FixedUpdate:
                    executorsFixedUpdateList.Remove(functionTimer);
                    break;
            }

            timersList.Remove(functionTimer);
            UpdateCount();
            functionTimer = null;
        }
        public List<ExecutorBase> GetExecutorTimer(string id)
        {
            return timersList.FindAll(executor => executor.Id.Equals(id));
        }

        private void Update()
        {
            for (int i = 0; i < executorsNormalUpdateList.Count; i++)
            {
                var executor = executorsNormalUpdateList[i];
                if (!executor.Paused) { executor.Update(Time.deltaTime); }
            }
        }
        private void LateUpdate()
        {
            for (int i = 0; i < executorsLateUpdateList.Count; i++)
            {
                var executor = executorsLateUpdateList[i];
                if (!executor.Paused) { executor.Update(Time.deltaTime); }
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < executorsFixedUpdateList.Count; i++)
            {
                var executor = executorsFixedUpdateList[i];
                if (!executor.Paused) { executor.Update(Time.fixedDeltaTime); }
            }
        }

        private void UpdateCount()
        {
            ExecutorsCount = timersList.Count;
        }
    }
}
