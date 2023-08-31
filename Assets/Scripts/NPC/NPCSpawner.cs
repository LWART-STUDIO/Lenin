using UnityEngine;
using Zenject;

namespace NPC
{
    public class NPCSpawner : MonoBehaviour
    {
        private DiContainer _container;
        public Transform SpawnPoint;
        public GameObject PrefabToSpawn;
        private Transform _playerSpawnPoint;
        

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public void Spawn(GameObject prefab,Transform spawnPoint)
        {
            PrefabToSpawn = prefab;
            SpawnPoint = spawnPoint;

            _container.InstantiatePrefab(
                PrefabToSpawn, SpawnPoint.position,
                Quaternion.identity, spawnPoint);
            
        }
    }
}
