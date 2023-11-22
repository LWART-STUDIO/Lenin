using CarterGames.Assets.SaveManager;
using PixelCrushers;
using UnityEngine;

namespace GameProgress
{
    [CreateAssetMenu(fileName = "Progress Data",menuName = "Add/Create Progress Data")]
    public class GameProgressData : SaveObject
    {
        public SaveValue<Location> CurrentLocation = new SaveValue<Location>("CurrentLocation");
        public SaveValue<Vector3> CurrentPlayerPosition = new SaveValue<Vector3>("CurrentPlayerPosition");
        public SaveValue<string> CurrentCutScene = new SaveValue<string>("CurrentCutScene","Start");
        public SaveValue<int> LastQuestIndex = new SaveValue<int>("LastQuestIndex");
        public SaveValue<int> MoneyCount = new SaveValue<int>("MoneyCount");
        public SaveValue<int> ParapharmCount = new SaveValue<int>("ParapharmCount",100);
    }
}