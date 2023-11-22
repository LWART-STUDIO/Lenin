using System;
using CustomInput;
using DG.Tweening;
using Location;
using Teleport.Map;
using UnityEngine;
using Zenject;
//REMOVE MAGIC
//REFACTOR THIS
namespace UI.Map
{
    [RequireComponent(typeof(MapPlayerAnimation))]
    public class MapPlayerMover : MonoBehaviour
    {
        private Tween _moveTween;
        private LocationsControl _locationsControl;
        private GameProgress.Location _nextLocation;
        private MapPlayerAnimation _mapPlayerAnimation;


        [Inject]
        private void Construct(LocationsControl locationsControl,
            CutSceneShower cutSceneShower,
            InputControl inputControl,
            UIControl uiControl)
        {
            _locationsControl = locationsControl;

        }

        private void Awake()
        {
            _mapPlayerAnimation = GetComponent<MapPlayerAnimation>();
        }

        public void TeleportToPoint(Vector3 position)
        {
            transform.position = position;
        }
        public void MoveToPoint(Vector3 position,GameProgress.Location location)
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
            }
                
            _nextLocation = location;
            _moveTween = transform.DOLocalMove(position, 5f).OnComplete(LoadNextLocation);
            _mapPlayerAnimation.Go();
        }

        private void LoadNextLocation()
        {
            _locationsControl.SwitchLocationAndShowBlackScreen(_nextLocation);
        }

       
    }
}
