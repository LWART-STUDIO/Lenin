using PixelCrushers;
using UnityEngine;

namespace GameProgress
{
    [CreateAssetMenu(fileName = "Progress Data",menuName = "Add/Create Progress Data")]
    public class GameProgressData : ScriptableObject
    {
        public Location CurrentLocation;
        public Vector3 CurrentPlayerPosition;
        public string CurrentCutScene;
        public SavedGameData SavedGameData;
    }
}