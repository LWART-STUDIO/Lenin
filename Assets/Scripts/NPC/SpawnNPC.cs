using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NPC
{
    public class SpawnNPC : MonoBehaviour
    {
        [SerializeField] private Transform _playerDefaultSpawnPoint;
        [SerializeField] private List<NPCSpawnData> _npcSpawnData;
        [SerializeField] private bool _spawnPatrolNpc;
        [SerializeField] private List<Transform> _patrolPoints;
        [SerializeField] private List<NPCQuestGiver> _npcPatrolPrefab;
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
            StartCoroutine(WaitToSpawn());
        }

        private IEnumerator WaitToSpawn()
        {
            for (int i = 0; i < _npcSpawnData.Count; i++)
            {
                _npcSpawner.Spawn(_npcSpawnData[i].Prefab,_npcSpawnData[i].SpawnPoint);
                yield return new WaitForSeconds(1f);
            }
            if(!_spawnPatrolNpc)
                yield break;
            for (int i = 0; i < _npcPatrolPrefab.Count; i++)
            {
                NPCQuestGiver npc = _npcSpawner.Spawn(_npcPatrolPrefab[i].gameObject,_patrolPoints[Random.Range(0,_patrolPoints.Count)]).GetComponent<NPCQuestGiver>();
                npc.StartPatrol(_patrolPoints);
                yield return new WaitForSeconds(3f);
                
            }
           
        }
    }
}
