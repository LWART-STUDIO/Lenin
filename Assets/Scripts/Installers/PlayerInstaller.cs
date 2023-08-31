using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public Transform PlayerSpawnPoint;
        public GameObject PlayerPrefab;
        
        public override void InstallBindings()
        {
            BindPlayer();
        }
        private void BindPlayer()
        {
            PlayerMarker playerMarker = Container
                .InstantiatePrefabForComponent<PlayerMarker>(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity,
                    null);
            playerMarker.gameObject.name = "Player";

            Container
                .Bind<PlayerMarker>()
                .FromInstance(playerMarker)
                .AsSingle();
        }
    }
}
