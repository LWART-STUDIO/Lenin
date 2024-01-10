using System.Collections;
using System.Collections.Generic;
using NPC.TrafficSystem;
using UnityEngine;
using Zenject;

namespace NPC
{
    public class SpawnNPC : MonoBehaviour
    {
        [SerializeField] private Transform _playerDefaultSpawnPoint;
        [SerializeField] private List<NPCSpawnData> _npcSpawnData;
        [SerializeField] private bool _spawnPatrolNpc;
        [SerializeField] private Transform _waypotsRoot;
        [SerializeField] private List<NPCQuestGiver> _npcPatrolPrefab;
        [SerializeField] private int _patrolNPCCount;
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
            int npcCounter = 0;
            for (int i = 0; i < _patrolNPCCount; i++)
            {
                Transform child = _waypotsRoot.GetChild(Random.Range(0, _waypotsRoot.childCount - 1));
                Waypoint waypoint = child.GetComponent<Waypoint>();
                NPCQuestGiver npc = _npcSpawner.Spawn(_npcPatrolPrefab[npcCounter].gameObject,child).GetComponent<NPCQuestGiver>();
                npcCounter++;
                if (npcCounter > _npcPatrolPrefab.Count - 1)
                    npcCounter = 0;
                if(waypoint!=null)
                    npc.StartPatrol(waypoint);
                else
                {
                    npc.DontMove();
                }
                yield return new WaitForSeconds(3f);
                
            }
           
        }
    }
}
