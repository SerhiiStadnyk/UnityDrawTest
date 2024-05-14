using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    public class GlobalEventsInitializer : MonoBehaviour
    {
        [SerializeField]
        private List<GlobalEventBase> _globalEvents;
        
        
        private void Start()
        {
            foreach (var globalEvent in _globalEvents)
            {
                globalEvent.Initialize();
            }
        }
    }
}