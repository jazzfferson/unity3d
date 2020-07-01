using UnityEngine;
using UnityEngine.Events;

namespace JazzDev.Events
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E: BaseGameEvent<T> where UER: UnityEvent<T>
    {
       [SerializeField] private E gameEvent;
       [SerializeField] private UER unityEventResponse;
        public E GameEvent { get => gameEvent; set => gameEvent = value; }

        private void OnEnable()
        {
            gameEvent?.RegisterListener(this);
        }
        private void OnDisable()
        {
            gameEvent?.UnregisterListener(this);
        }
        public void Invoke(T item)
        {
            unityEventResponse?.Invoke(item);
        }
    }
}

