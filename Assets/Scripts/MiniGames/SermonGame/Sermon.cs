using UnityEngine;
using Zenject;

namespace MiniGames.SermonGame
{
    public class Sermon : MonoBehaviour
    {
        private int _cost;
        private Inventory.Inventory _inventory;

        [Inject]
        private void Construct(Inventory.Inventory inventory)
        {
            _inventory = inventory;
        }
        
        public void StartSermon()
        {
            _inventory.GetMoney(_cost);
        }
    }
}
