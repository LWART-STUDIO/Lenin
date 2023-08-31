using System;
using Location;
using PixelCrushers;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace GameProgress
{
    public class GameProgressControl : MonoBehaviour
    {
        [SerializeField] private GameProgressData _progressData;

        private LocationsControl _locationsControl;
        private CutSceneShower _cutSceneShower;
        private PlayerMarker _playerMarker;
        [Inject]
        private void Construct(LocationsControl locationsControl,
            CutSceneShower cutSceneShower,
            PlayerMarker playerMarker)
        {
            _locationsControl = locationsControl;
            _cutSceneShower = cutSceneShower;
            _playerMarker = playerMarker;

        }

        [NaughtyAttributes.Button()]
        public void LoadDataToScene()
        {
            CheckProgressData();
            LoadCutScene();
            LoadLocation();
            SpawnPlayer();
            
           
        }

        private void LoadLocation() => 
            _locationsControl.SpawnLocation(_progressData.CurrentLocation);

        private void LoadCutScene()
        {
            if(!string.IsNullOrEmpty(_progressData.CurrentCutScene))
                _cutSceneShower.ShowCutscene(_progressData.CurrentCutScene);
        }

        [NaughtyAttributes.Button()]
        public void Save()
        {
        }

        private void SpawnPlayer()
        {
            Transform playerSpawnPoint = _locationsControl.GetCurrentLocation().SpawnNpc.GetPlayerSpawnPoint();
            _playerMarker.transform.position = playerSpawnPoint != null
                ? playerSpawnPoint.position : _progressData.CurrentPlayerPosition;
        }
        private void CheckProgressData()
        {
            if(_progressData==null)
                _progressData = ScriptableObject.CreateInstance<GameProgressData>();
        }
    }
}
