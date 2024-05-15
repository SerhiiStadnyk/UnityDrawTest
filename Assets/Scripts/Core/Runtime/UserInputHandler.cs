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
        private const string RIGHT_BUTTON = "RightButton";

        private InputAction _leftMouseAction;
        
        private InputAction _rightMouseAction;

        private Coroutine _leftMousePressedCoroutine;
        
        private Coroutine _rightMousePressedCoroutine;

        public event Action OnLeftMouseButton;
        public event Action OnLeftMouseButtonDown;
        public event Action OnLeftMouseButtonUp;
        
        public event Action OnRightMouseButton;


        protected void Awake()
        {
            _leftMouseAction = _playerControls.FindActionMap(MOUSE).FindAction(LEFT_BUTTON);
            _rightMouseAction = _playerControls.FindActionMap(MOUSE).FindAction(RIGHT_BUTTON);
        }


        private void OnEnable()
        {
            _leftMouseAction.Enable();
            _rightMouseAction.Enable();
            RegisterInputActions();
        }


        private void OnDisable()
        {
            _leftMouseAction.Disable();
            _rightMouseAction.Disable();
            UnregisterInputActions();
        }


        private void RegisterInputActions()
        {
            _leftMouseAction.performed += LeftMouseDown;
            _leftMouseAction.canceled += LeftMouseUp;
            
            _rightMouseAction.performed += RightMouseDown;
        }


        private void UnregisterInputActions()
        {
            _leftMouseAction.performed -= LeftMouseDown;
            _leftMouseAction.canceled -= LeftMouseUp;
            
            _rightMouseAction.performed -= RightMouseDown;
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
        
        
        private void RightMouseDown(InputAction.CallbackContext context)
        {
            _rightMousePressedCoroutine ??= StartCoroutine(RightMousePressedRoutine());
        }
        
        
        private IEnumerator RightMousePressedRoutine()
        {
            while (_rightMouseAction.IsPressed())
            {
                OnRightMouseButton?.Invoke();
                yield return null;
            }

            _rightMousePressedCoroutine = null;
        }
    }
}
