using UnityEngine;

namespace Core.Runtime
{
    public abstract class BrushBase : MonoBehaviour
    {
        public abstract void Paint(Vector2 uv, Texture2D texture);


        public abstract void StopPainting();


        public virtual void Select()
        {
        }


        public virtual void Deselect()
        {
        }
    }
}
