using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime
{
    public class GlobalObserverAction : MonoBehaviour
    {
        [SerializeField]
        private GlobalEventAction _eventAction;

        [SerializeField]
        private UnityEvent _unityEvent;


        private void Start()
        {
            _eventAction.SubscribeObserver(this);
        }


        public void TriggerObserver()
        {
            _unityEvent?.Invoke();
        }
    }
}
