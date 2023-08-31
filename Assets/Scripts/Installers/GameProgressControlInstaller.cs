using GameProgress;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameProgressControlInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gameProgressControl;
        public override void InstallBindings()
        {
            GameProgressControl gameProgressControl = Container
                .InstantiatePrefabForComponent<GameProgressControl>(_gameProgressControl, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<GameProgressControl>()
                .FromInstance(gameProgressControl)
                .AsSingle();
            gameProgressControl.LoadDataToScene();
        }
    }
}