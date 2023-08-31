using CustomInput;
using Quest;
using UI;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "DialogEvents",menuName = "Add/Dialog/DialogEvents")]
    public class DialogEvents : ScriptableObject
    {
        private QuestController _questController;
        private UIControl _uiControl;
        private InputControl _inputControl;
        [Inject]
        private void Construct(QuestController questController,
            UIControl uiControl,
            InputControl inputControl)
        {
            _questController = questController;
            _uiControl = uiControl;
            _inputControl = inputControl;
        }
        public void UpdateQuest()
        {
            
            _questController.SetCurrentQuest();
        }

        public void UpdateQuestUI()
        {
            _uiControl.SetUpQuestPanel(_questController.GetCurrentQuest());
        }

        public void BlockInput()
        {
            _inputControl.BlockAllInput();
        }

        public void UnlockInput()
        {
            _inputControl.UnlockAllInput();
        }

        public void ShowMap()
        {
            _uiControl.OpenMap();
        }
    }
}
