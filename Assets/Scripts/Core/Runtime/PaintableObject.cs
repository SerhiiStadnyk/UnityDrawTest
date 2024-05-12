using UnityEngine;

namespace Core.Runtime
{
    public class PaintableObject : MonoBehaviour, IPaintable
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public MeshRenderer MeshRenderer => _meshRenderer;
    }
}
