
namespace JazzDev.Executor
{
    public class Once : Executor<Once>
    {
        public Once(string id, float delay, EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(id, delay), updateType) { }
        public Once(ExecutorParameters parameters, EUpdateType updateType = EUpdateType.Normal) : base(parameters, updateType) { }
        public Once(EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(), updateType) { }

        public override void Update(float time)
        {
            if (ended || paused)
                return;
            if (timer >= 0)
            {
                timer -= time;

                if (timer <= 0)
                {
                    ended = true;
                    executeCallBack?.Invoke(this);
                    if (parameters.autoDestroy) { Destroy(parameters.destroyCallOnFinish); }
                }
            }
        }
    }
}
