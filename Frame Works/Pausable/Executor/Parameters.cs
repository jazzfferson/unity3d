
namespace JazzDev.Executor
{
    public struct ExecutorParameters
    {
        public string id;
        public float defaultDelay;
        public int iterations;
        public float maxDelay;
        public bool isRandomDelay;
        public bool autoDestroy;
        public bool destroyCallOnFinish;

        public ExecutorParameters(string id = "",
            float defaultDelay = 0,
            bool isRandomDelay = false,
            float maxDelay = 1,
            int iterations = 1,
            bool autoDestroy = true,
            bool destroyCallOnFinish = true)
        {
            this.id = id;
            this.defaultDelay = defaultDelay;
            this.iterations = iterations;
            this.maxDelay = maxDelay;
            this.isRandomDelay = isRandomDelay;
            this.autoDestroy = autoDestroy;
            this.destroyCallOnFinish = destroyCallOnFinish;
        }
    }
}
