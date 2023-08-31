using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NPC
{
    public class SpawnNPC : MonoBehaviour
    {
        [SerializeField] private Transform _playerDefaultSpawnPoint;
        [SerializeField] private List<NPCSpawnData> _npcSpawnData;
        private NPCSpawner _npcSpawner;
        

        [Inject]
        private void Construct(NPCSpawner npcSpawner)
        {
            _npcSpawner = npcSpawner;
        }

        public Transform GetPlayerSpawnPoint()
        {
            return _playerDefaultSpawnPoint;
        }

        private void Awake()
        {
            for (int i = 0; i < _npcSpawnData.Count; i++)
            {
                _npcSpawner.Spawn(_npcSpawnData[i].Prefab,_npcSpawnData[i].SpawnPoint);
            }
        }
    }
}
