using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Core.Runtime
{
    public class BrushPencil : BrushBase
    {
        [SerializeField] 
        private int _brushSize = 1;
        
        [SerializeField] 
        private Color _brushColor = Color.red;

        [SerializeField]
        private UnityEvent<float> _sizeChangedEventFloat;
        
        [SerializeField]
        private UnityEvent<string> _sizeChangedEventString;

        private Vector2? _previousPosition;

        private UserInputHandler _userInputHandler;


        [Inject]
        public void Inject(UserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }


        public void SetSize(float size)
        {
            _brushSize = (int)size;
            _sizeChangedEventFloat?.Invoke(_brushSize);
            _sizeChangedEventString?.Invoke(_brushSize.ToString());
        }
        
        
        public void SetSize(string size)
        {
            int.TryParse(size, out _brushSize);
            _sizeChangedEventFloat?.Invoke(_brushSize);
            _sizeChangedEventString?.Invoke(_brushSize.ToString());
        }


        public void SetColor(Color color)
        {
            _brushColor = color;
        }


        public override void Paint(Vector2 uv, Texture2D texture)
        {
            Vector2 textUV = new Vector2(uv.x * texture.width, uv.y * texture.height);

            if (_previousPosition.HasValue)
            {
                foreach (var pixelUV in PaintingLogic.GetPointsOnLine(_previousPosition.Value, textUV))
                {
                    PaintingLogic.DrawCircle((int)pixelUV.x, (int)pixelUV.y, _brushSize, texture, _brushColor);
                }
            }

            PaintingLogic.DrawCircle((int)textUV.x, (int)textUV.y, _brushSize, texture, _brushColor);

            _previousPosition = textUV;
            texture.Apply();
        }


        public override void Select()
        {
            base.Select();
            _userInputHandler.OnLeftMouseButtonUp += StopPainting;
        }


        public override void Deselect()
        {
            base.Deselect();
            _userInputHandler.OnLeftMouseButtonUp -= StopPainting;
        }
        
        
        private void StopPainting()
        {
            _previousPosition = null;
        }
    }
}
