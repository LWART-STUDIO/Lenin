using TMPro;
using UnityEngine;

namespace UI
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;

        public void ClearText()
        {
            SetUpText("", "");
        }
        public void SetUpText(Quest.Quest quest)
        {
            if(quest.Visible) 
                SetUpText(quest.Title, quest.Description);
            else
                ClearText();
        }

        public void SetUpText(string title, string description)
        {
            _title.text = title;
            _description.text = description;
        }
    }
}
