
using System;
using System.Collections.Generic;

namespace JazzDev.Executor
{

    public enum EExecutorDirection { Forward, Backward };

    [Serializable]
    public struct ExecutorParameters
    {
        public string id;
        public float defaultDelay;
        public int loops;
        public float maxDelay;
        public float duration;
        public bool destroyOnFinish;
        public bool keepBetweenScenes;
        public EExecutorDirection direction;
        public bool destroyCallOnFinish;


        public ExecutorParameters(string id = "",
            float defaultDelay = 0,
            float maxDelay = 1,
            int loops = 0,
            float duration = 1,
            bool destroyOnFinish = true,
            bool keepBetweenScenes = false,
            EExecutorDirection direction = EExecutorDirection.Forward,
            bool destroyCallOnFinish = true)
        {

            this.id = id;
            this.defaultDelay = defaultDelay;
            this.loops = loops;
            this.maxDelay = maxDelay;
            this.duration = duration;
            this.destroyOnFinish = destroyOnFinish;
            this.keepBetweenScenes = keepBetweenScenes;
            this.direction = direction;
            this.destroyCallOnFinish = destroyCallOnFinish;
        }
    }

    public interface IParameter
    {
        (string name, string value) GetInfo();
    }

    public abstract class Parameter<T> : IParameter
    {
        protected string name;
        private T value;

        public T Value { get => value; set => this.value = value; }

        public Parameter(string name, T value)
        {
            this.value = value;
            this.name = name;
        }

        public (string name, string value) GetInfo()
        {
            return (name, value.ToString());
        }
    }
    public class IntParam : Parameter<int>
    {
        public IntParam(string name, int value) : base(name,value)
        {
           
        }
    }
    public class FloatParam : Parameter<float>
    {
        public FloatParam(string name,float value) : base(name,value)
        {

        }
    }
    public class BoolParam : Parameter<bool>
    {
        public BoolParam(string name, bool value) : base(name,value)
        {

        }
    }
    public class StringParam : Parameter<string>
    {
        public StringParam(string name, string value) : base(name,value)
        {

        }
    }
    public class DirectionParam : Parameter<EExecutorDirection>
    {
        public DirectionParam(string name, EExecutorDirection value) : base(name,value)
        {

        }
    }
    public class UpdateTypeParam : Parameter<EUpdateType>
    {
        public UpdateTypeParam(string name, EUpdateType value) : base(name, value)
        {
        }
    }
}
