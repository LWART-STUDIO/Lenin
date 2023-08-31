using NPC;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class NPCSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private NPCSpawner _npcSpawner;
        
        public override void InstallBindings()
        {
            Container
                .Bind<NPCSpawner>()
                .FromInstance(_npcSpawner)
                .AsSingle();
        }
    }
}
