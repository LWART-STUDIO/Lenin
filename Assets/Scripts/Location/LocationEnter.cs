using System;
using UI;
using UnityEngine;
using Zenject;

namespace Location
{
    public class LocationEnter : MonoBehaviour
    {
        [SerializeField] private GameProgress.Location _locationToEnter;
        private UIControl _uiControl;
        private LocationsControl _locationsControl;

        [Inject]
        private void Construct(UIControl uiControl,
            LocationsControl locationsControl)
        {
            _uiControl = uiControl;
            _locationsControl = locationsControl;
        }

        public void ShowLocationEnterButton()
        {
            _uiControl.ButtonsControl.EnterButton.gameObject.SetActive(true);
            _uiControl.ButtonsControl.EnterButton.onClick.AddListener(delegate { Enter(); });
        }
        public void HideLocationEnterButton()
        {
            _uiControl.ButtonsControl.EnterButton.gameObject.SetActive(false);
        }

        public void Enter()
        {
            HideLocationEnterButton();
            _locationsControl.SwitchLocationAndShowBlackScreen(_locationToEnter);
        }
    }
}
