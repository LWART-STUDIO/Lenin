using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "Add/Quests/Quests Data")]
    [Serializable]
    public class QuestsData : ScriptableObject
    {
        public List<Quest> Quests = new List<Quest>();
    }
}