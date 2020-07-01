using System;
public interface IFinishEvent<T>
{
     event Action<T> OnFinishEvent;
     T AddFinish(Action<T> callback);
}
