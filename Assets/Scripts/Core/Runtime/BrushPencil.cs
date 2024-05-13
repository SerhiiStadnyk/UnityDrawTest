using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class BrushPencil : BrushBase
    {
        [SerializeField] 
        private int _brushSize = 1;
        
        [SerializeField] 
        private Color _brushColor = Color.red;
        
        private Vector2? _previousPosition;
        private PaintingLogic _paintingLogic;
        
        private UserInputHandler _userInputHandler;


        [Inject]
        public void Inject(UserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }


        private void Awake()
        {
            _paintingLogic = new PaintingLogic();
        }


        public override void Paint(Vector2 uv, Texture2D texture)
        {
            Vector2 textUV = new Vector2(uv.x * texture.width, uv.y * texture.height);

            if (_previousPosition.HasValue)
            {
                foreach (var pixelUV in _paintingLogic.GetPointsOnLine(_previousPosition.Value, textUV))
                {
                    _paintingLogic.DrawCircle((int)pixelUV.x, (int)pixelUV.y, _brushSize, texture, _brushColor);
                }
            }

            _paintingLogic.DrawCircle((int)textUV.x, (int)textUV.y, _brushSize, texture, _brushColor);

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
