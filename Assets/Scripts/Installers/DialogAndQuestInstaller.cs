using PixelCrushers.DialogueSystem;
using Quest;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DialogAndQuestInstaller: MonoInstaller
    {

        [SerializeField] private DialogueSystemController _dialog;
        [SerializeField] private QuestController _questController;
        public override void InstallBindings()
        {
            BindQuest();
            BindDialog();
        }


        private void BindDialog()
        {
            /*DialogueSystemController dialogueSystem = Container
                .InstantiatePrefabForComponent<DialogueSystemController>(_dialog, Vector3.zero, Quaternion.identity,
                    null);*/

            Container
                .Bind<DialogueSystemController>()
                .FromInstance(_dialog)
                .AsSingle();
        }
        private void BindQuest()
        {
            /*DialogueSystemController dialogueSystem = Container
                .InstantiatePrefabForComponent<DialogueSystemController>(_dialog, Vector3.zero, Quaternion.identity,
                    null);*/

            Container
                .Bind<QuestController>()
                .FromInstance(_questController)
                .AsSingle();
        }
    }
}