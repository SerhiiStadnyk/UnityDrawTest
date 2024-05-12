using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Runtime
{
    public class UserInputHandler : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _playerControls;
        
        private const string MOUSE = "Mouse"; 
        private const string LEFT_BUTTON = "LeftButton";

        private InputAction _leftMouseAction;

        private Coroutine _leftMousePressedCoroutine;

        public event Action OnLeftMouseButton;
        public event Action OnLeftMouseButtonDown;
        public event Action OnLeftMouseButtonUp;


        protected void Awake()
        {
            _leftMouseAction = _playerControls.FindActionMap(MOUSE).FindAction(LEFT_BUTTON);
        }


        private void OnEnable()
        {
            _leftMouseAction.Enable();
            RegisterInputActions();
        }


        private void OnDisable()
        {
            _leftMouseAction.Disable();
            UnregisterInputActions();
        }


        private void RegisterInputActions()
        {
            _leftMouseAction.performed += LeftMouseDown;
            _leftMouseAction.canceled += LeftMouseUp;
        }


        private void UnregisterInputActions()
        {
            _leftMouseAction.performed -= LeftMouseDown;
            _leftMouseAction.canceled -= LeftMouseUp;
        }
        
        
        private IEnumerator LeftMousePressedRoutine()
        {
            while (_leftMouseAction.IsPressed())
            {
                OnLeftMouseButton?.Invoke();
                yield return null;
            }

            _leftMousePressedCoroutine = null;
        }
        
        
        private void LeftMouseDown(InputAction.CallbackContext context)
        {
            OnLeftMouseButtonDown?.Invoke();
            _leftMousePressedCoroutine ??= StartCoroutine(LeftMousePressedRoutine());
        }


        private void LeftMouseUp(InputAction.CallbackContext context)
        {
            OnLeftMouseButtonUp?.Invoke();
        }
    }
}
