using UnityEngine;

namespace Core.Runtime
{
    public interface IPaintable
    {
        public MeshRenderer MeshRenderer { get; }
        
        public GameObject GameObject { get; }
    }
}
