using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private UserInputHandler _userInputHandler;
        
        
        public override void InstallBindings()
        {
            Container.BindInstance(_userInputHandler).AsSingle();
        }
    }
}
