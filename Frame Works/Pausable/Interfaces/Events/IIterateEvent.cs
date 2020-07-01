using System;
public interface IIterateEvent<T>
{
     event Action<T> OnIterateEvent;
     T SetIterate(Action<T> callback);
}

