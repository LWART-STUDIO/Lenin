using System;
using Quest;
using UnityEngine;
using Zenject;

namespace NPC
{
    public class NPCQuestGiver : MonoBehaviour
    {
        [SerializeField] private NPCType _npcType;
        private QuestController _questController;

        [Inject]
        private void Construct(QuestController questController)
        {
            _questController = questController;
        }

        private void Awake()
        {
            SetUpNpc();
        }

        private void SetUpNpc()
        {

        }
        
        public void TryToContact()
        {
            if (_questController.GetCurrentQuest().AssignedCharacterName != _npcType)
                return;
            if (_questController.GetCurrentQuest().Status != QuestStatus.NotStarted)
                return;
            _questController.GetCurrentQuest().Status = QuestStatus.InProgress;
        }
    }
}