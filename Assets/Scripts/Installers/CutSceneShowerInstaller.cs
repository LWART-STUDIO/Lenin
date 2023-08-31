using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CutSceneShowerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _cutSceneCanvas;
        public override void InstallBindings()
        {
            CutSceneShower cutSceneShower = Container
                .InstantiatePrefabForComponent<CutSceneShower>(_cutSceneCanvas, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<CutSceneShower>()
                .FromInstance(cutSceneShower)
                .AsSingle();
        }
    }
}