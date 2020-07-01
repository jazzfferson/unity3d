using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
namespace JazzDev.Executor
{
    public class ExecutorManager : MonoBehaviour
    {

        private List<ExecutorBase> timersList;
        private List<ExecutorBase> executorsNormalUpdateList;
        private List<ExecutorBase> executorsLateUpdateList;
        private List<ExecutorBase> executorsFixedUpdateList;
        public static bool _applicationIsQuitting = false;

        public event Action UpdateEditorWindowsEvent;

        [SerializeField] private int executorsCount;

        public List<ExecutorBase> TimersList { get => timersList; }

        private void Awake()
        {
            timersList = new List<ExecutorBase>();
            executorsNormalUpdateList = new List<ExecutorBase>();
            executorsLateUpdateList = new List<ExecutorBase>();
            executorsFixedUpdateList = new List<ExecutorBase>();
            DontDestroyOnLoad(this.gameObject);
        }

        public void Add(ExecutorBase executor)
        {
            timersList.Add(executor);
            // switch (executor.updateType)
            switch (executor.updateType.Value)
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
            }

            UpdateCount();
        }

        public void Destroy(ExecutorBase executor)
        {
            //switch (executor.updateType)
            switch (executor.updateType.Value)
            {
                case EUpdateType.Normal:
                    executorsNormalUpdateList.Remove(executor);
                    break;
                case EUpdateType.LateUpdate:
                    executorsLateUpdateList.Remove(executor);
                    break;
                case EUpdateType.FixedUpdate:
                    executorsFixedUpdateList.Remove(executor);
                    break;
            }

            timersList.Remove(executor);
            UpdateCount();
            executor = null;
        }

        public void Destroy(string id)
        {
            List<ExecutorBase> listExecutors;

            if (GetExecutors(id, out listExecutors))
            {
                for (int i = listExecutors.Count - 1; i >= 0; i--)
                {
                    Destroy(listExecutors[i]);
                }
            }
        }

        public bool GetExecutors(string id, out List<ExecutorBase> executorsList)
        {
            executorsList = timersList.FindAll(executor => executor.Id.Equals(id));
            return executorsList == null ? false : executorsList.Count > 0 ? true : false;
        }


        /// <summary>
        /// Deleta os Executors que estão marcados para não passarem de uma cena para outra.
        /// </summary>
        public void CleanExecutorsBetweenScenes()
        {
            for (int i = timersList.Count - 1; i >= 0; i--)
            {
                if (!timersList[i].KeepBetweenScenes.Value)
                    Destroy(timersList[i]);
            }
        }

        private float timer;

        private void Update()
        {
            for (int i = 0; i < executorsNormalUpdateList.Count; i++)
            {
                var executor = executorsNormalUpdateList[i];
                if (!executor.Pause && !executor.HasEnded && executor.IsAlive) { executor.Update(Time.deltaTime); }
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1;
                UpdateEditorWindowsEvent?.Invoke();
            }
        }
        private void LateUpdate()
        {
            for (int i = 0; i < executorsLateUpdateList.Count; i++)
            {
                var executor = executorsLateUpdateList[i];
                if (!executor.Pause && !executor.HasEnded && executor.IsAlive) { executor.Update(Time.deltaTime); }
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < executorsFixedUpdateList.Count; i++)
            {
                var executor = executorsFixedUpdateList[i];
                if (!executor.Pause && !executor.HasEnded && executor.IsAlive) { executor.Update(Time.fixedDeltaTime); }
            }
        }
        private void UpdateCount()
        {
            executorsCount = timersList.Count;
        }

        private void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
            Destroy(gameObject);
        }
    }
}
