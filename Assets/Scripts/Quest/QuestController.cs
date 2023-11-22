using System;
using UI;
using UnityEngine;
using Zenject;

namespace Quest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private QuestsData _questsData;
        [SerializeField] private Quest _currentQuest;
        private QuestsData _mainData;
        private UIControl _uiControl;

        [Inject]
        private void Construct(UIControl uiControl)
        {
            _uiControl = uiControl;
        }
        public void SetData(int questIndex)
        {
            if (_mainData == null) _mainData = Instantiate(_questsData);
            for (int i = 0; i < _mainData.Quests.Count; i++)
            {
                if (i < questIndex)
                {
                    if(questIndex>0 && questIndex<_mainData.Quests.Count)
                        _mainData.Quests[i].QuestDataLoad(QuestStatus.Completed);
                }
            }
            SetCurrentQuest();
        }

        public int GetCurrentQuestIndex()
        {
            for (int i = 0; i < _mainData.Quests.Count; i++)
            {
                    if (_mainData.Quests[i].Status == QuestStatus.NotStarted)
                        return i;
            }
            return 0;
        }
        

        public void SetCurrentQuest()
        {
            for (int i = 0; i < _mainData.Quests.Count; i++)
            {
                Quest quest = _mainData.Quests[i];
                if (i > 0 && _mainData.Quests[i - 1].Status != QuestStatus.Completed)
                    _mainData.Quests[i - 1].Status = QuestStatus.Completed;
                if (quest.Status == QuestStatus.NotStarted)
                {
                    _currentQuest = quest;
                   _uiControl.SetUpQuestPanel(_currentQuest);
                    return;
                }
            }
        }

        public Quest GetCurrentQuest()
        {
            return _currentQuest;
        }
        public  Quest GetQuest(string tag)
        {
            return _mainData.Quests.Find(q => q.Tag == tag);
        }
        public bool TryGetQuest(string tag, out Quest quest)
        {
            quest = GetQuest(tag);

            return quest != null;
        }

        
    }
}
