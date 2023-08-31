using NPC;
using UnityEngine;

namespace Location
{
    [RequireComponent(typeof(SpawnNPC))]
    public class LocationMarker : MonoBehaviour
    {
        private SpawnNPC _spawnNpc;
        [SerializeField] private GameProgress.Location _location;
        public GameProgress.Location Location => _location;
        public SpawnNPC SpawnNpc => _spawnNpc;

        private void Awake()
        {
            _spawnNpc = GetComponent<SpawnNPC>();
        }
    }
}
