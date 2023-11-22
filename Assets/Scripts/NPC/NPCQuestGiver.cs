using System;
using System.Collections.Generic;
using Location;
using PixelCrushers.DialogueSystem;
using Quest;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NPCQuestGiver : MonoBehaviour
    {
        [SerializeField] private NPCType _npcType;
        [SerializeField] private NPCMover _npcMover;
        [SerializeField] private string[] _miniDialogs;
        [SerializeField] private string[] _photoDialogs;
        [SerializeField] private Sprite _iconSprite;
        private LocationsControl _locationsControl;
        private QuestController _questController;

        [Inject]
        private void Construct(QuestController questController,
            LocationsControl locationsControl)
        {
            _questController = questController;
            _locationsControl = locationsControl;
        }

        public void StartPatrol(List<Transform> patrolPoints)
        {
            _npcMover.StartPatrol(patrolPoints);
        }
        private void Awake()
        {
            SetUpNpc();
        }

        private void SetUpNpc()
        {

        }

        public NPCType GetType()
        {
            return _npcType;
        }
        
        public void TryToContact()
        {
            if(DialogueManager.instance.activeConversations.Count>0)
                return;
            if (_questController.GetCurrentQuest().AssignedCharacterName != _npcType)
            {
                TryDialog();
                return;
            }
            if (_questController.GetCurrentQuest().Status != QuestStatus.NotStarted)
            {
                TryDialog();
                return;
            }
            _questController.GetCurrentQuest().Status = QuestStatus.InProgress;
        }

        private void TryDialog()
        {
            Debug.Log("ChangeDialogue");
            if (_locationsControl.GetCurrentLocationType() == GameProgress.Location.RedSquare)
            {
                if(_photoDialogs==null||_photoDialogs.Length==0)
                    PhotoDialog();
                    
            }
            else
            {
                MiniDialog();
            }
            
        }

        private void PhotoDialog()
        {
            DialogueManager.instance.StartConversation(_photoDialogs[Random.Range(0,_photoDialogs.Length)]);
            DialogueManager.instance.SetActorPortraitSprite("People", _iconSprite);
            DialogueManager.instance.conversationEnded+=_npcMover.ContinueMove;
            _npcMover.PauseMove(null);
        }
        private void MiniDialog()
        {
            if(_miniDialogs==null||_miniDialogs.Length==0)
                return;
            DialogueManager.instance.StartConversation(_miniDialogs[Random.Range(0,_miniDialogs.Length)]);
            DialogueManager.instance.SetActorPortraitSprite("People", _iconSprite);
            DialogueManager.instance.conversationEnded+=_npcMover.ContinueMove;
            _npcMover.PauseMove(null);
        }
        
    }
}