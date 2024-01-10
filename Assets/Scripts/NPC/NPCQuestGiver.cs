using System;
using System.Collections.Generic;
using Location;
using NPC.TrafficSystem;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using Quest;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using DialogueManager = Dialogues.DialogueManager;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NPCQuestGiver : TagMaskEvent
    {
        [SerializeField] private NPCType _npcType;
        [SerializeField] private NPCMover _npcMover;
        [SerializeField] private string[] _miniDialogs;
        [SerializeField] private string[] _defaultDialogs;
        [SerializeField] private string[] _photoDialogs;
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private string _photoCunsceneName;
        [SerializeField] private GameObject _interactiveSprite;
        private LocationsControl _locationsControl;
        private QuestController _questController;
        private bool _mouseDown;

        [Inject]
        private void Construct(QuestController questController,
            LocationsControl locationsControl)
        {
            _questController = questController;
            _locationsControl = locationsControl;
        }

        public void StartPatrol(Waypoint waypoint)
        {
            _npcMover.GetComponent<WaypointNavigator>().StartPatrol(waypoint);
        }

        public void DontMove()
        {
            _npcMover.UpdatePoint();
        }
        private void Awake()
        {
            SetUpNpc();
        }

        private void SetUpNpc()
        {

        }
        
        public NPCType GetNPCType()
        {
            return _npcType;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!IsInTagMask(other.tag))
                return;
            if(DialogueManager.instance.activeConversations.Count>0)
                return;
            if (_npcType != NPCType.People && _questController.GetCurrentQuest().AssignedCharacterName != _npcType)
                return;
           
            _interactiveSprite.SetActive(true);
            if (Input.GetKey(KeyCode.E) || _mouseDown)
            {
                TryToContact();
            }
            
          
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!IsInTagMask(other.tag))
                return;
            _interactiveSprite.SetActive(false);
            
        }

        private void OnMouseDown()
        {
            _mouseDown = true;
        }

        private void OnMouseUp()
        {
            _mouseDown = false;
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
            if (_questController.GetCurrentQuest().Tag == "TalkWithPeople")
                DialogueManager.instance.conversationStarted += SetSpriteAfterTime;
            _questController.GetCurrentQuest().Status = QuestStatus.InProgress;
           
        }

        private void SetSpriteAfterTime(Transform transform)
        {
            DialogueManager.instance.SetActorPortraitSprite("People", _iconSprite);
            DialogueManager.instance.conversationStarted -= SetSpriteAfterTime;
        }
        private void TryDialog()
        {
            Debug.Log("ChangeDialogue");
            if (_locationsControl.GetCurrentLocationType() == GameProgress.Location.RedSquare)
            {
                if (_photoDialogs == null || _photoDialogs.Length == 0)
                {
                    MiniDialog();
                    return;
                }
                    
                PhotoDialog();
            }
            else
            {
                
                if (_miniDialogs!=null&&_miniDialogs.Length!=0)
                {
                    MiniDialog();
                }
                else if(_defaultDialogs!=null&&_defaultDialogs.Length!=0)
                {
                   DefaultDialog();
                }
            }
            
        }

        private void PhotoDialog()
        {
            if(_photoDialogs==null||_photoDialogs.Length==0)
                return;
            DialogueManager.instance.StartConversation(_photoDialogs[Random.Range(0,_photoDialogs.Length)]);
            DialogueManager.instance.SetActorPortraitSprite("People", _iconSprite);
            DialogueManager.instance.databaseManager.masterDatabase.variables[1].InitialValue = _photoCunsceneName;
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

        private void DefaultDialog()
        {
            if(_defaultDialogs==null||_defaultDialogs.Length==0)
                return;
            DialogueManager.instance.StartConversation(_defaultDialogs[Random.Range(0,_defaultDialogs.Length)]);
        }
    }
}