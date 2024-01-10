using PixelCrushers.DialogueSystem;
using UnityEngine;
using DialogueManager = Dialogues.DialogueManager;

namespace Inventory
{
    public class Money : InventoryItem
    {
        public void UpdateMoneyInDialogSystem()
        {
            DialogueManager.instance.databaseManager.masterDatabase.variables.Find(x=>x.Name=="Money").InitialFloatValue = GetCount();
            Debug.Log(DialogueManager.instance.databaseManager.masterDatabase.variables.Find(x=>x.Name=="Money").InitialFloatValue);
           
        }
    }
}
