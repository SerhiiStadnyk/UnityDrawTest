using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 5f;

        private UserInputHandler _userInputHandler;


        [Inject]
        public void Inject(UserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }


        private void Start()
        {
            _userInputHandler.OnRightMouseButton += Rotate;
        }


        private void Rotate()
        {
            // Rotate the object while the right mouse button is held down
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate rotation amount based on mouse movement
            float rotationAmountX = mouseX * _rotationSpeed;
            float rotationAmountY = mouseY * _rotationSpeed;

            // Rotate the object
            transform.Rotate(Vector3.up, -rotationAmountX, Space.World);
            transform.Rotate(Vector3.right, rotationAmountY, Space.World);
        }
    }
}