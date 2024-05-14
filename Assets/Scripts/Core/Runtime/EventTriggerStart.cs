using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime
{
    public class EventTriggerStart : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<float> _eventFloat;

        [SerializeField]
        private float _floatValue;


        private void Start()
        {
            _eventFloat?.Invoke(_floatValue);
        }
    }
}
