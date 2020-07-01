using System;

public interface IPausable<T>
{
    bool Pause { get; set; }
    event Action<T> OnPauseChangeCallBack;
}
