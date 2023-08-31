using System;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class NPCSpawnData
    {
        public GameObject Prefab;
        public Transform SpawnPoint;
    }
}