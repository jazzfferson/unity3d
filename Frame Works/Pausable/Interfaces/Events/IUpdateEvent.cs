using System;
public interface IUpdateEvent<T>
{
     event Action<T> OnUpdateEvent;
     T AddUpdate(Action<T> callback);
}

