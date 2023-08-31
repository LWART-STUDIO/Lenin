using System.Collections.Generic;
using Dialogs;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "TestInstallerSO", menuName = "Add/TestInstallerSO")]

    public class DialogEventsInstaller : ScriptableObjectInstaller<DialogEventsInstaller>

    {
        [SerializeField] private DialogEvents _dialogEvents;

        public override void InstallBindings()
        {
            Container.QueueForInject(_dialogEvents);
        }
    }
}