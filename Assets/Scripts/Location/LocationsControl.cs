using CustomInput;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Location
{
    public class LocationsControl : MonoBehaviour
    {
        [SerializeField] private LocationMarker _redSquareStart;
        [SerializeField] private LocationMarker _redSquare;
        [SerializeField] private LocationMarker _hospital;
        [SerializeField] private LocationMarker _morgue;
        [SerializeField] private LocationMarker _factory;
        [SerializeField] private LocationMarker _dumb;
        [SerializeField] private LocationMarker _rublevka;
        [SerializeField] private LocationMarker _bank;
        [SerializeField] private LocationMarker _churchInside;
        [SerializeField] private LocationMarker _churchOutside;
        [SerializeField] private LocationMarker _chemetry;
        [SerializeField] private LocationMarker _crypt;
        private LocationMarker _currentLocation;
        private DiContainer _container;
        private PlayerMarker _playerMarker;
        private CutSceneShower _cutSceneShower;
        private InputControl _inputControl;
        private UIControl _uiControl;
        private GameProgress.Location _nextLocation;
        

        [Inject]
        private void Construct(DiContainer container,
            PlayerMarker playerMarker,
            CutSceneShower cutSceneShower,
            UIControl uiControl,
            InputControl inputControl)
        {
            _container = container;
            _playerMarker = playerMarker;
            _cutSceneShower = cutSceneShower;
            _inputControl = inputControl;
            _uiControl = uiControl;

        }

        public LocationMarker GetCurrentLocation()
        {
            return _currentLocation;
        }

        public GameProgress.Location GetCurrentLocationType()
        {
            return _currentLocation.Location;
        }

        private void SpawnPlayer()
        {
            Transform playerSpawnPoint = GetCurrentLocation().SpawnNpc.GetPlayerSpawnPoint();
            _playerMarker.transform.position = playerSpawnPoint != null
                ? playerSpawnPoint.position : _playerMarker.transform.position;
        }
        public void SwitchLocationAndShowBlackScreen(GameProgress.Location location)
        {
            _nextLocation = location;
            _inputControl.BlockAllInput();
            _uiControl.HideMap();
            _cutSceneShower.ShowBlackLoadingScreen();
            Invoke(nameof(SwitchLocation),0.7f);
        }

        public void SwitchLocationWithoutBlackScreen(GameProgress.Location location)
        {
            _nextLocation = location;
            SwitchLocation();
        }

        private void SwitchLocation()
        {
            SpawnLocation(_nextLocation);
        }

        public void SpawnLocation(GameProgress.Location location)
        {
            if (_currentLocation != null)
            {
                Destroy(_currentLocation.gameObject);
            }
            switch (location)
            {
                case GameProgress.Location.RedSquareStart:
                    _currentLocation = _container.InstantiatePrefab(_redSquareStart).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.RedSquare:
                    _currentLocation = _container.InstantiatePrefab(_redSquare).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Hospital:
                    _currentLocation =  _container.InstantiatePrefab(_hospital).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Morgue:
                    _currentLocation = _container.InstantiatePrefab(_morgue).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Bank:
                    _currentLocation = _container.InstantiatePrefab(_bank).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Chemetery:
                    _currentLocation = _container.InstantiatePrefab(_chemetry).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Dumb:
                    _currentLocation = _container.InstantiatePrefab(_dumb).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Factory:
                    _currentLocation = _container.InstantiatePrefab(_factory).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Rublevka:
                    _currentLocation = _container.InstantiatePrefab(_rublevka).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.ChurchInside:
                    _currentLocation = _container.InstantiatePrefab(_churchInside).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.ChurchOutside:
                    _currentLocation = _container.InstantiatePrefab(_churchOutside).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                case GameProgress.Location.Crypt:
                    _currentLocation = _container.InstantiatePrefab(_crypt).GetComponent<LocationMarker>();
                    SpawnPlayer();
                    break;
                    
            }
            
        }
        
    }
}
