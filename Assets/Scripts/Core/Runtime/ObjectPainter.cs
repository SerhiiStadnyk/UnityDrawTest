using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ObjectPainter : MonoBehaviour
    {
        [SerializeField]
        private string _texturePropertyName;
        
        private Camera _mainCamera;
        private Texture2D _decalTexture;
        private IPaintable _paintable;
        private BrushBase _activeBrush;

        private UserInputHandler _userInputHandler;

        public Texture2D DecalTexture => _decalTexture;


        [Inject]
        public void Inject(UserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }
        
        
        public void SetBrush(BrushBase brush)
        {
            if (_activeBrush != null)
            {
                _activeBrush.Deselect();
            }

            _activeBrush = brush;
            _activeBrush.Select();
        }


        public void ClearTexture()
        {
            if (_paintable != null && _decalTexture != null)
            {
                PaintingLogic.Fill(_decalTexture, new Color(0, 0, 0, 0));
            }
        }


        public void SetTexture(Texture2D texture)
        {
            if (_paintable != null && _decalTexture != null)
            {
                _decalTexture = texture;
                _paintable.MeshRenderer.material.SetTexture(_texturePropertyName, _decalTexture);
            }
        }


        private void Start()
        {
            _mainCamera = Camera.main;
        }


        public void InitPaintableObject(IPaintable paintable)
        {
            _paintable = paintable;
            
            //Create a copy of object material to avoid modifying the original material
            Material originalMaterial = _paintable.MeshRenderer.sharedMaterial;
            Material newMaterial = new Material(originalMaterial.shader);
            newMaterial.CopyPropertiesFromMaterial(originalMaterial);
            _paintable.MeshRenderer.material = newMaterial;
            
            //Create copy of decal texture and set to new material
            _decalTexture = Instantiate((Texture2D)_paintable.MeshRenderer.material.GetTexture(_texturePropertyName));
            _paintable.MeshRenderer.material.SetTexture(_texturePropertyName, _decalTexture);
        }


        private void OnEnable()
        {
            _userInputHandler.OnLeftMouseButton += Paint;
        }
        
        
        private void OnDisable()
        {
            _userInputHandler.OnLeftMouseButton -= Paint;

            if (_activeBrush != null)
            {
                _activeBrush.Deselect();
            }
        }


        private void Paint()
        {
            if (_activeBrush != null && Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                if (hitInfo.transform.gameObject == _paintable.GameObject)
                {
                    _activeBrush.Paint(hitInfo.textureCoord, _decalTexture);
                }
            }
            else if (_activeBrush != null)
            {
                _activeBrush.StopPainting();
            }
        }
    }
}
