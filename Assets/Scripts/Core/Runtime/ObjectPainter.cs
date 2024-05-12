using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ObjectPainter : MonoBehaviour
    {
        [SerializeField] 
        private int _brushSize = 1;
        
        [SerializeField] 
        private Color _brushColor = Color.red;

        [SerializeField]
        private PaintableObject _paintableObject;
        
        private Camera _mainCamera;
        private Texture2D _canvasTexture;
        private IPaintable _paintable;
        
        private Vector2? _previousPosition;

        private UserInputHandler _userInputHandler;


        [Inject]
        public void Inject(UserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }


        private void Start()
        {
            _mainCamera = Camera.main;

            _paintable = _paintableObject;
            
            _canvasTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
            _paintable.MeshRenderer.material.mainTexture = _canvasTexture;
        }


        private void OnEnable()
        {
            _userInputHandler.OnLeftMouseButton += Paint;
            _userInputHandler.OnLeftMouseButtonUp += StopPainting;
        }
        
        
        private void OnDisable()
        {
            _userInputHandler.OnLeftMouseButton -= Paint;
            _userInputHandler.OnLeftMouseButtonUp -= StopPainting;
        }


        private void Paint()
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                PaintOnMesh(hitInfo.textureCoord);
            }
        }


        private void StopPainting()
        {
            _previousPosition = null;
        }


        private void PaintOnMesh(Vector2 uv)
        {
            Vector2 textUV = new Vector2(uv.x * _canvasTexture.width, uv.y * _canvasTexture.height);

            if (_previousPosition.HasValue)
            {
                foreach (var pixelUV in GetPointsOnLine(_previousPosition.Value, textUV))
                {
                    DrawCircle((int)pixelUV.x, (int)pixelUV.y, _brushSize);
                }
            }

            DrawCircle((int)textUV.x, (int)textUV.y, _brushSize);

            _previousPosition = textUV;
            _canvasTexture.Apply();
        }

        
        private void DrawCircle(int centerX, int centerY, int radius)
        {
            int sqrRadius = radius * radius;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (x * x + y * y <= sqrRadius)
                    {
                        int texX = centerX + x;
                        int texY = centerY + y;

                        if (texX >= 0 && texX < _canvasTexture.width && texY >= 0 && texY < _canvasTexture.height)
                        {
                            _canvasTexture.SetPixel(texX, texY, _brushColor);
                        }
                    }
                }
            }
        }

        
        private IEnumerable<Vector2> GetPointsOnLine(Vector2 p1, Vector2 p2)
        {
            float distance = Vector2.Distance(p1, p2);
            int steps = Mathf.CeilToInt(distance);
            for (int i = 0; i <= steps; i++)
            {
                yield return Vector2.Lerp(p1, p2, i / distance);
            }
        }
    }
}
