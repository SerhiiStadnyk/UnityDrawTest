using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private UserInputHandler _userInputHandler;

        [SerializeField]
        private ObjectPainter _objectPainter;
        
        
        public override void InstallBindings()
        {
            Container.BindInstance(_userInputHandler).AsSingle();
            Container.BindInstance(_objectPainter).AsSingle();
        }
    }
}
