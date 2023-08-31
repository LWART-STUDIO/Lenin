using CustomInput;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InputControlInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputControlPrefab;
        public override void InstallBindings()
        {
            InputControl inputControl = Container
                .InstantiatePrefabForComponent<InputControl>(_inputControlPrefab, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<InputControl>()
                .FromInstance(inputControl)
                .AsSingle();

        }
    }
}
