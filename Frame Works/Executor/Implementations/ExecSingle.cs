
using System;
using UnityEngine;

namespace JazzDev.Executor
{
    public class ExecSingle : ExecutorOnce<ExecSingle>
    {
        public ExecSingle(float delay = 0, string id = "ExecutorOnce", EUpdateType updateType = EUpdateType.Normal) : base(delay,id,/*new ExecutorParameters(id, delay),*/ updateType)
        {
          this.executorType.Value = "ExecSingle";
        }
    }
}
