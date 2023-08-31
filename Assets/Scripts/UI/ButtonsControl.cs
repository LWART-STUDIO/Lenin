using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ButtonsControl : MonoBehaviour
    {
        [SerializeField] private Button _mapButton;
        [SerializeField] private Button _enterButton;
        public Button EnterButton => _enterButton;
        private UIControl _uiControl;

        [Inject]
        private void Construct(UIControl uiControl)
        {
            _uiControl = uiControl;
        }
        private void Awake()
        {
            _mapButton.onClick.AddListener(delegate { _uiControl.OpenMap(); });
        }
    }
}
