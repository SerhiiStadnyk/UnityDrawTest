using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    //A class for initializing project. Ideally state machine should be utilized, but that would be overkill for such a small test project.
    public class ProjectInitiator : MonoBehaviour
    {
        [SerializeField]
        private PaintableObject _paintableObject;

        private ObjectPainter _objectPainter;


        [Inject]
        public void Inject(ObjectPainter objectPainter)
        {
            _objectPainter = objectPainter;
        }


        private void Start()
        {
            _objectPainter.InitPaintableObject(_paintableObject);
        }
    }
}
