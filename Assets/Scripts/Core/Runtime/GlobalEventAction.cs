using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "GlobalEvent_Action", menuName = "Core/GlobalEvents/Action", order = 1)]
    public class GlobalEventAction : GlobalEventBase
    {
        private List<GlobalObserverAction> _observers = new List<GlobalObserverAction>();


        public override void Initialize()
        {
            _observers = new List<GlobalObserverAction>();
        }


        public void TriggerEvent()
        {
            foreach (GlobalObserverAction observer in _observers)
            {
                observer.TriggerObserver();
            }
        }


        public void SubscribeObserver(GlobalObserverAction observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }


        public void UnsubscribeObserver(GlobalObserverAction observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
    }
}
