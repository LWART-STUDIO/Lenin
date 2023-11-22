using System.Collections.Generic;
using Location;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Map
{
    public class MapControl : MonoBehaviour
    {
        [SerializeField] private MapPlayerMover _mapPlayer;
        [SerializeField] private List<MapLocation> _locations;
        private LocationsControl _locationsControl;


        [Inject]
        private void Construct(LocationsControl locationsControl)

        {
            _locationsControl = locationsControl;
        }

        private void Awake()
        {
            for (int i = 0; i < _locations.Count; i++)
            {
                int iCopy = i;
                _locations[i].GetComponent<Button>().onClick.AddListener(delegate {MovePlayer(_locations[iCopy]);  });
            }
        }

        private void OnEnable()
        {
            var currentLocation = _locationsControl.GetCurrentLocation().Location;
            Debug.Log($"Teleport to {currentLocation} on map.");
            for (int i = 0; i < _locations.Count; i++)
            {
                if (currentLocation == _locations[i].Location)
                {
                    _mapPlayer.TeleportToPoint(_locations[i].PositionToMove.position);
                    return;
                }
            }

            switch (currentLocation)
            {
                case GameProgress.Location.RedSquareStart:
                    _mapPlayer.TeleportToPoint(_locations.Find(x=>
                        x.Location==GameProgress.Location.RedSquare).PositionToMove.position);
                    return;
                case GameProgress.Location.Morgue:
                    _mapPlayer.TeleportToPoint(_locations.Find(x=>
                        x.Location==GameProgress.Location.Hospital).PositionToMove.position);
                    return;
                case GameProgress.Location.ChurchInside:
                    _mapPlayer.TeleportToPoint(_locations.Find(x=>
                        x.Location==GameProgress.Location.ChurchOutside).PositionToMove.position);
                    return;
                case GameProgress.Location.Crypt:
                    _mapPlayer.TeleportToPoint(_locations.Find(x=>
                        x.Location==GameProgress.Location.Chemetery).PositionToMove.position);
                    return;

            }
            _mapPlayer.TeleportToPoint(_locations.Find(x=>
                x.Location==GameProgress.Location.RedSquare).PositionToMove.position);
        }

        
        private void MovePlayer(MapLocation location)
        {
            Debug.Log($"Move to {location.Location} on map.");
            _mapPlayer.MoveToPoint(location.PositionToMove.localPosition+location.transform.localPosition,location.Location);

        }
    }
}
