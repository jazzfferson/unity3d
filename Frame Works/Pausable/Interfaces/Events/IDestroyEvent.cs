using System;
public interface IDestroyEvent<T>
{
     event Action<T> OnDestroyEvent;
     T AddDestroy(Action<T> callback);
}

