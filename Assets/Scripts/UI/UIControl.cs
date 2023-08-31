using CustomInput;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private GameObject _mapCanvas;
        [SerializeField] private ButtonsControl _buttonsCanvas;
        [SerializeField] private QuestPanel _questPanel;
        public ButtonsControl ButtonsControl => _buttonsCanvas;
        private InputControl _inputControl;
        [Inject]
        private void Construct(InputControl inputControl)
        {
            _inputControl = inputControl;
            _inputControl.OpenMap += OpenMap;
        }

        public void SetUpQuestPanel(Quest.Quest quest)
        {
            Debug.Log("ChangeUi");
            _questPanel.SetUpText(quest);
        }
        public void HideMap()
        {
            _mapCanvas.SetActive(false);
            _inputControl.UnlockMoveInput();
        }
            
        public void OpenMap()
        {
            _mapCanvas.SetActive(!_mapCanvas.activeSelf);
            if (_mapCanvas.activeSelf)
                _inputControl.BlockMoveInput();
            else
                _inputControl.UnlockMoveInput();

        }
    }
}
