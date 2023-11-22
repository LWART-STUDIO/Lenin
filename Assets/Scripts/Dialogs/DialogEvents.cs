using CustomInput;
using Location;
using NPC;
using Quest;
using UI;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "DialogEvents",menuName = "Add/Dialog/DialogEvents")]
    public class DialogEvents : ScriptableObject
    {
        private QuestController _questController;
        private UIControl _uiControl;
        private InputControl _inputControl;
        private CutSceneShower _cutSceneShower;
        private LocationsControl _locationsControl;
        private Inventory.Inventory _inventory;
        [Inject]
        private void Construct(QuestController questController,
            UIControl uiControl,
            InputControl inputControl,
            CutSceneShower cutSceneShower,
            LocationsControl locationsControl,
            Inventory.Inventory inventory)
        {
            _questController = questController;
            _uiControl = uiControl;
            _inputControl = inputControl;
            _cutSceneShower = cutSceneShower;
            _locationsControl = locationsControl;
            _inventory = inventory;
        }
        public void UpdateQuest()
        {
            
            _questController.SetCurrentQuest();
        }

        public void UpdateQuestUI()
        {
            _uiControl.SetUpQuestPanel(_questController.GetCurrentQuest());
        }

        public void BlockInput()
        {
            _inputControl.BlockAllInput();
        }

        public void UnlockInput()
        {
            _inputControl.UnlockAllInput();
        }

        public void ShowMap()
        {
            _uiControl.OpenMap();
        }

        public void DestroyTruman()
        {
            NPCQuestGiver[] npcQuestGivers = FindObjectsOfType<NPCQuestGiver>();
            for (int i = 0; i < npcQuestGivers.Length; i++)
            {
                if(npcQuestGivers[i].GetType()==NPCType.Truman)
                    Destroy(npcQuestGivers[i].gameObject);
            }
        }

        public void ShowCutScene(string name)
        {
            _cutSceneShower.ShowCutscene(name);
        }

        public void SwithLoacationWithoutBlackScreen(int locationIndex)
        {
            _locationsControl.SwitchLocationWithoutBlackScreen((GameProgress.Location)locationIndex);
        }
        public void SwithLoacationWithBlackScreen(int locationIndex)
        {
            _locationsControl.SwitchLocationAndShowBlackScreen((GameProgress.Location)locationIndex);
        }

        public void MakeQuestInProgress()
        {
            _questController.GetCurrentQuest().Status = QuestStatus.InProgress;

        }

        public void GiveMoney()
        {
            _inventory.GiveMoney(15);
        }
    }
}
