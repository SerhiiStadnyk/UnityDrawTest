using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "GlobalEvent_Color", menuName = "Core/GlobalEvents/Color", order = 1)]
    public class GlobalEventColor : GlobalEventBase
    {
        private List<GlobalObserverColor> _observers = new List<GlobalObserverColor>();


        public override void Initialize()
        {
            _observers = new List<GlobalObserverColor>();
        }


        public void TriggerEvent(Color color)
        {
            foreach (GlobalObserverColor observer in _observers)
            {
                observer.TriggerObserver(color);
            }
        }


        public void SubscribeObserver(GlobalObserverColor observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }


        public void UnsubscribeObserver(GlobalObserverColor observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
    }
}