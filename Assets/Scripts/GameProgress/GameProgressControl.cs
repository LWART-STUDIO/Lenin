using CarterGames.Assets.SaveManager;
using Location;
using Player;
using Quest;
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
        private QuestController _questController;
        private Inventory.Inventory _inventory;
        [Inject]
        private void Construct(LocationsControl locationsControl,
            CutSceneShower cutSceneShower,
            PlayerMarker playerMarker,
            QuestController questController,
            Inventory.Inventory inventory)
        {
            _locationsControl = locationsControl;
            _cutSceneShower = cutSceneShower;
            _playerMarker = playerMarker;
            _questController = questController;
            _inventory = inventory;

        }

        [NaughtyAttributes.Button()]
        public void LoadDataToScene()
        {
            SaveManager.Load();
            _progressData.Load();
            CheckProgressData();
            _questController.SetData(_progressData.LastQuestIndex.Value);
            LoadCutScene();
            LoadLocation();
            SpawnPlayer();
            LoadInventory();

        }
        [NaughtyAttributes.Button()]
        public void ResetSave()
        {
            _progressData.ResetObjectSaveValues();
            SaveManager.Save();
           
        }

        private void LoadInventory()
        {
            _inventory.SetUpMoney(_progressData.MoneyCount.Value);
            _inventory.SetUpParapharm(_progressData.ParapharmCount.Value);
        }

        private void SaveInventory()
        {
            _progressData.MoneyCount.Value = _inventory.GetMoneyCount();
            _progressData.ParapharmCount.Value = _inventory.GetParapharmCount();
        }
        private void LoadLocation() => 
            _locationsControl.SpawnLocation(_progressData.CurrentLocation.Value);

        private void LoadCutScene()
        {
            if(!string.IsNullOrEmpty(_progressData.CurrentCutScene.Value))
                _cutSceneShower.ShowCutscene(_progressData.CurrentCutScene.Value);
        }

        [NaughtyAttributes.Button()]
        public void Save()
        {
            if (_progressData.CurrentCutScene.Value == "Start")
                _progressData.CurrentCutScene.Value = "";
            GetLocation();
            GetPlayerPosition();
            LoadQuestIndex();
            SaveInventory();
            _progressData.Save();
            SaveManager.Save();
        }

        private void LoadQuestIndex()
        {
            _progressData.LastQuestIndex.Value = _questController.GetCurrentQuestIndex();
            Debug.Log(_progressData.LastQuestIndex.Value);

        }
        private void SpawnPlayer()
        {
            Transform playerSpawnPoint = _locationsControl.GetCurrentLocation().SpawnNpc.GetPlayerSpawnPoint();
            _playerMarker.transform.position = playerSpawnPoint != null
                ? playerSpawnPoint.position : _progressData.CurrentPlayerPosition.Value;
        }

        private void GetPlayerPosition()
        {
            _progressData.CurrentPlayerPosition.Value = _playerMarker.transform.position;
        }
        private void GetLocation() =>
            _progressData.CurrentLocation.Value = _locationsControl.GetCurrentLocation().Location;

        private void CheckProgressData()
        {
            if(_progressData==null)
                _progressData = ScriptableObject.CreateInstance<GameProgressData>();
        }

        private void GivePPharapharm()
        {
            _inventory.GiveParaphrm(100);
        }
        [NaughtyAttributes.Button]
        private void GiveSomeMoney()
        {
            _inventory.GiveMoney(100);
        }
    }
}
