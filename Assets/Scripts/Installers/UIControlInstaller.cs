using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIControlInstaller:MonoInstaller
    {
        [SerializeField] private UIControl _uiControl;
        public override void InstallBindings()
        {
            Container
                .Bind<UIControl>()
                .FromInstance(_uiControl)
                .AsSingle();
        }
    }
}