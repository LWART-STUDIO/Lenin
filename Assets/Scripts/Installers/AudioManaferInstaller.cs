using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class AudioManaferInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_cutSceneCanvas")] [SerializeField] private GameObject _audioManagerPrefab;
        public override void InstallBindings()
        {
            AudioManager cutSceneShower = Container
                .InstantiatePrefabForComponent<AudioManager>(_audioManagerPrefab, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<AudioManager>()
                .FromInstance(cutSceneShower)
                .AsSingle();
        }
    }
}
