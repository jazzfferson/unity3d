
namespace JazzDev.Executor
{
    public class Updator : Executor<Updator>
    {
        private bool endedDelay = false;

        public Updator(EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(), updateType)
        {
        }
        public Updator(ExecutorParameters parameters, EUpdateType updateType = EUpdateType.Normal) : base(parameters, updateType)
        {
        }
        public Updator(string id, float initialDelay, EUpdateType updateType = EUpdateType.Normal) : base(new ExecutorParameters(id, initialDelay), updateType)
        {
        }
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
            endedDelay = false;
        }

        public override void Update(float time)
        {
            if (ended || paused)
                return;

            if (timer >= 0)
            {
                timer -= time;
                if (timer <= 0) { endedDelay = true; }
            }
            else if (endedDelay)
            {
                executeCallBack?.Invoke(this);
            }
        }
    }
}
