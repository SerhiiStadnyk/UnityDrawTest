using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "GlobalEvent_Color", menuName = "Core/GlobalEvents/Color", order = 1)]
    public class GlobalEventColor : GlobalEventBase
    {
        private List<GlobalEventObserverColor> _observers = new List<GlobalEventObserverColor>();


        public override void Initialize()
        {
            _observers = new List<GlobalEventObserverColor>();
        }


        public void TriggerEvent(Color color)
        {
            foreach (GlobalEventObserverColor observer in _observers)
            {
                observer.TriggerObserver(color);
            }
        }


        public void SubscribeObserver(GlobalEventObserverColor observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }


        public void UnsubscribeObserver(GlobalEventObserverColor observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
    }
}