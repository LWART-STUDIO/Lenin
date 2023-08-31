using UnityEngine;

namespace UI.Map
{
    public class MapLocation : MonoBehaviour
    {
        [SerializeField] private RectTransform _playerPosition;
        public RectTransform PositionToMove => _playerPosition;
        public GameProgress.Location Location;
    }
}
