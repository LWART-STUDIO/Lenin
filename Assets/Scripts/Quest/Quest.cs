using System;
using System.Collections.Generic;
using NPC;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using Zenject;

namespace Quest
{
    [Serializable]
    public class Quest
    {
        public string Tag;

        public string Title;
        public string Description;

        [SerializeField] private QuestStatus _status;
        public QuestStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;

                    foreach (QuestEvent questEvent in questEvents)
                    {
                        if (questEvent.StatusExecute == _status)
                        {
                            questEvent.onStatusChanged += (questEvent) =>
                            {
                                switch (questEvent.Function)
                                {
                                    case QuestEventFunction.None:
                                        break;
                                    case QuestEventFunction.StartDialog:
                                        ChangeDialogue(questEvent);
                                        break;
                                    
                                }
                            };
                            questEvent.onStatusChanged.Invoke(questEvent);
                        }
                    }
                }
            }
        }


        public NPCType AssignedCharacterName;

        public bool Visible;

        public List<QuestEvent> questEvents = new();
        

        /*[Inject]
        private void Construct(
            DialogueSystemController conversationManager)
        {
            _dialogueSystem = conversationManager;
        }*/

        public Quest(string questTag, string questTitle, string questDescription, QuestStatus status, NPCType assignedCharacterName, bool visible)
        {
            this.Tag = questTag;
            this.Title = questTitle;
            this.Description = questDescription;
            this.Status = status;
            this.AssignedCharacterName = assignedCharacterName;
            this.Visible = visible;
        }

        public void QuestDataLoad(QuestStatus status)
        {
            this.Status = status;
        }
        
        
        
        private void ChangeDialogue(QuestEvent questEvent)
        {
            Debug.Log("ChangeDialogue");
            DialogueManager.instance.StartConversation(questEvent.TitleOfDialog);
        }
        
    }

    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

}
