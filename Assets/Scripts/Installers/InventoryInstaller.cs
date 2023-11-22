using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _inventoryPrefab;
        public override void InstallBindings()
        {
            Inventory.Inventory inventory = Container
                .InstantiatePrefabForComponent<Inventory.Inventory>(_inventoryPrefab, Vector3.zero, Quaternion.identity,
                    null);

            Container
                .Bind<Inventory.Inventory>()
                .FromInstance(inventory)
                .AsSingle();

        }
    }
}