using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public abstract class BrushBase : MonoBehaviour
    {
        private ObjectPainter _objectPainter;
        
        
        [Inject]
        public void Inject(ObjectPainter objectPainter)
        {
            _objectPainter = objectPainter;
        }


        public abstract void Paint(Vector2 uv, Texture2D texture);


        public virtual void Select()
        {
            _objectPainter.SetBrush(this);
        }


        public virtual void Deselect()
        {
        }
    }
}
