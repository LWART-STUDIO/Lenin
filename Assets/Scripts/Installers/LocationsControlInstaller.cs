using GameProgress;
using Location;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocationsControlInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _locationControl;
        public override void InstallBindings()
        {
            LocationsControl locationsControl = Container
                .InstantiatePrefabForComponent<LocationsControl>(_locationControl, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<LocationsControl>()
                .FromInstance(locationsControl)
                .AsSingle();
        }
    }
}