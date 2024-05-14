using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime
{
    public class GlobalEventObserverColor : MonoBehaviour
    {
        [SerializeField]
        private GlobalEventColor _eventColor;

        [SerializeField]
        private UnityEvent<Color> _unityEvent;


        private void Start()
        {
            _eventColor.SubscribeObserver(this);
        }


        public void TriggerObserver(Color color)
        {
            _unityEvent?.Invoke(color);
        }
    }
}
