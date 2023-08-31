using System;
using NaughtyAttributes;
using UnityEngine.Events;

namespace Quest
{
    [Serializable]
    public class QuestEvent
    {
        public QuestStatus StatusExecute;

        public QuestEventFunction Function;

        public delegate void OnStatusChangedDelegate(QuestEvent quest);

        public OnStatusChangedDelegate onStatusChanged;
        [ShowIf("Function",QuestEventFunction.StartDialog)]
        public string TitleOfDialog;


        //public NPCs NPC;
    }

    public enum QuestEventFunction
    {
        None = 0,
        StartDialog = 1,

    }
}
