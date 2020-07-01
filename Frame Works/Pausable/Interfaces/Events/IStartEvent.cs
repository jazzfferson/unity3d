using System;
public interface IStartEvent<T>
{
    event Action<T> OnStartEvent;
    T AddStart(Action<T> callback);
}